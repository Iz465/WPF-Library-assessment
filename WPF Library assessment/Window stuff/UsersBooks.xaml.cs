using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;
using WPF_Library_assessment.Window_stuff;
using static WPF_Library_assessment.User_Control_Stuff.bookCardUC;

namespace WPF_Library_assessment.Window_stuff
{
    
    public partial class UsersBooks : Window
    {
        public UsersBooks()
        {
            InitializeComponent();

          showOrderedBooks();



        }
        private void showOrderedBooks()
        {
            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> scifiBooks = mongoData.Connect<Books>("Sci-Fi");
            List<Books> mysteryBooks = mongoData.Connect<Books>("Mystery");
            List<Books> graphicBooks = mongoData.Connect<Books>("Romance");
            List<Books> historyBooks = mongoData.Connect<Books>("History");

            int row = addInfo(horrorBooks, 0);
            row = addInfo(fantasyBooks, row);
            row = addInfo(scifiBooks, row);
            row = addInfo(mysteryBooks, row);
            row = addInfo(graphicBooks, row);
            row = addInfo(historyBooks, row);
            ReturnBookBtn(row);
        }

        public int addInfo(List<Books> BookType, int row)
        {
            BookPG bookPG = new BookPG();
            foreach (var book in BookType)
            {
                var user = signInWn.SessionManager.CurrentUser;

                if (user is WPF_Library_assessment.Mongo_Info.Members members)
                {
                    if (members.Username == book.Owner)
                    {
                        
                     
                        RowDefinition newRow = new RowDefinition();
                        newRow.Height = new GridLength(60);
                        InfoGrid.RowDefinitions.Add(newRow);
                        TextBlock nameText = bookPG.CreateTextBlock(book.Name);
                        Button returnSpecificBook = bookPG.CreateButton("Return"); returnSpecificBook.Click += (sender, e) => returnSpecific(sender, e, book);


                        Grid.SetRow(nameText, row);
                        Grid.SetColumn(nameText, 0);
                        Grid.SetColumnSpan(nameText, 2);
                        Grid.SetRow(returnSpecificBook, row);
                        Grid.SetColumn(returnSpecificBook,1);
                        InfoGrid.Children.Add(nameText);
                        InfoGrid.Children.Add(returnSpecificBook);
                        row++;
                     
                    }

                }





            }
          
            return row;
        }

        public void returnSpecific(object sender, RoutedEventArgs e, Books book) // havent figured out how to reset time for individual one with 
        {
            MongoData mongoData = new MongoData();
            var dataBase = mongoData.GetMongoDatabase();
            var collections = mongoData.getCollections();

            string collectionName = collections.FirstOrDefault(col => mongoData.Connect<Books>(col).Any(b => b.Id == book.Id)); // im using this so i dont repeat the collections after i have found the id, otherwise itll start bugging out trying to find an id that can only be found once

            IMongoCollection<Books> collection = dataBase.GetCollection<Books>(collectionName);
            var filter = Builders<Books>.Filter.Eq("_id", book.Id);

            var update = Builders<Books>.Update
                .Set("Owner", string.Empty)
                .Set("Overdue", "No")
                .Set("Available", "Yes")
                .Set("Time", 300); 

            var result = collection.UpdateOne(filter, update);

         
                MessageBox.Show("Book has been returned!");
                MongoData.BookReturnNotifier.NotifyBooksReturned();
            

            if (book.PreBookOwner != string.Empty)
            {
                bookCardUC bookCardUC = new bookCardUC();
                bookCardUC.prebookConvert(book, collection, collectionName, book.Time);
            }

            Button returnButton = (Button)sender;
            int row = Grid.GetRow(returnButton);

            List<UIElement> elementsToRemove = new List<UIElement>();
            foreach (UIElement element in InfoGrid.Children.Cast<UIElement>())
            {
                if (Grid.GetRow(element) == row)
                {
                    elementsToRemove.Add(element);
                }
            }

            foreach (var element in elementsToRemove)
            {
                InfoGrid.Children.Remove(element);
            }

            InfoGrid.RowDefinitions.RemoveAt(row);
        }



        public void ReturnBookBtn(int row)

        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = GridLength.Auto;
            InfoGrid.RowDefinitions.Add(newRow);
            Button newButton= new Button();
            newButton.Content = "Return All Books";
            newButton.Style = (Style)FindResource("mainbutton");
            newButton.Click += removeBooksBtn;


            Grid.SetRow(newButton, row);
            Grid.SetColumnSpan(newButton, 2);
            InfoGrid.Children.Add(newButton);
           

           
        }

        public void removeBooksBtn(object sender, RoutedEventArgs e)
        {
            MongoData mongoData = new MongoData();
            var dataBase = mongoData.GetMongoDatabase();
            var user = signInWn.SessionManager.CurrentUser;

            if (user is WPF_Library_assessment.Mongo_Info.Members members)
            {
                var collections = mongoData.getCollections();
 

                foreach (var collectionName in collections)
                {
                    IMongoCollection<Books> collection = dataBase.GetCollection<Books>(collectionName);
                    var filter = Builders<Books>.Filter.Eq("Owner", members.Username);
                    bookCardUC bookCardUC = new bookCardUC();
                    var returnedBooks = collection.Find(filter).ToList();

                    foreach (var book in returnedBooks)
                    {
                    
                     

                        var update = Builders<Books>.Update
                            .Set("Owner", string.Empty)
                            .Set("Overdue", "No")
                            .Set("Available", "Yes")
                            .Set("Time", 300);

                        collection.UpdateMany(filter, update);

                        if (book.PreBookOwner != string.Empty)
                        {
                            bookCardUC.prebookConvert(book, collection, collectionName, book.Time);
                        }
                    }
                }

                // Stop all timers
                TimerManager.StopAllTimers();

                try
                {
                    MessageBox.Show("Books Returned");
                    MongoData.BookReturnNotifier.NotifyBooksReturned();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void orderCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}




          

       