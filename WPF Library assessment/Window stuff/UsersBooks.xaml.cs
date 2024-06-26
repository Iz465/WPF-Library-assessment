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
            foreach (var book in BookType)
            {
                var user = signInWn.SessionManager.CurrentUser;

                if (user is WPF_Library_assessment.Mongo_Info.Members members)
                {
                    if (members.Username == book.Owner)
                    {
                        
                        RowDefinition newRow = new RowDefinition();
                        newRow.Height = new GridLength(40);
                        InfoGrid.RowDefinitions.Add(newRow);
                        TextBlock nameText = new TextBlock();
                        nameText.Text = book.Name;
                        nameText.FontSize = 16;
                        nameText.Margin = new Thickness(10, 0, 0, 0);
                        nameText.HorizontalAlignment = HorizontalAlignment.Center;
                        nameText.VerticalAlignment = VerticalAlignment.Center;

                        Grid.SetRow(nameText, row);
                        InfoGrid.Children.Add(nameText);
                        row++;
                     
                    }

                }





            }
          
            return row;
        }


        public void ReturnBookBtn(int row)

        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = GridLength.Auto;
            InfoGrid.RowDefinitions.Add(newRow);
            Button newButton= new Button();
            newButton.Content = "Return Books";
            newButton.Style = (Style)FindResource("mainbutton");
            newButton.Click += removeBooksBtn;


            Grid.SetRow(newButton, row);
            InfoGrid.Children.Add(newButton);
           

           
        }

        public void removeBooksBtn(object sender, RoutedEventArgs e)
        {
            MongoData mongoData = new MongoData();
            var dataBase = mongoData.GetMongoDatabase();
            var user = signInWn.SessionManager.CurrentUser;

            if (user is WPF_Library_assessment.Mongo_Info.Members members)
            {
                var collections = new List<string> { "Horror", "Fantasy", "Sci-Fi", "Mystery", "Romance", "History" };

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




          

       