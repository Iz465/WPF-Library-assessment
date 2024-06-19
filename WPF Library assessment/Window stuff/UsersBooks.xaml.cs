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

namespace WPF_Library_assessment.Window_stuff
{
    
    public partial class UsersBooks : Window
    {
        public UsersBooks()
        {
            InitializeComponent();

          showOrderedBooks();



        }

        public int addInfo(List<Books> BookType, int row)
        {
            foreach (var book in BookType)
            {
                var user = signInWn.SessionManager.CurrentUser;

                if (user is WPF_Library_assessment.Window_stuff.Members members)
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

            if (user is WPF_Library_assessment.Window_stuff.Members members)
            {
                var collections = new List<string> { "Horror", "Fantasy", "Drama", "Mystery" };

                foreach (var collectionName in collections)
                {
                    IMongoCollection<Books> collection = dataBase.GetCollection<Books>(collectionName);
                    var filter = Builders<Books>.Filter.Eq("Owner", members.Username);

                    var returnedBooks = collection.Find(filter).ToList();
                    bookCardUC bookCardUC = new bookCardUC();

                    foreach (var book in returnedBooks)
                    {

                        var update = Builders<Books>.Update
                        .Set("Owner", string.Empty)
                        .Set("Overdue", "No")
                        .Set("Available", "Yes");
                          //  .Set("Time", 300)

                            collection.UpdateMany(filter, update);
                        try
                        {
                            MessageBox.Show("hi");
                            var update1 = Builders<Books>.Update.Set("Time", 333);
                            collection.UpdateMany(filter, update1);
                        } catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                 
                            if (book.PreBookOwner != string.Empty)
                            {
                                bookCardUC.prebookConvert(book, collection, collectionName, book.Time);

                            }
                       
                        
                        }
                }

                MessageBox.Show("Books returned.");
                try
                {
                    MongoData.BookReturnNotifier.NotifyBooksReturned();
                }
                catch (Exception ex)
                {
           
                }
           
            }
        }


        private void showOrderedBooks()
        {
          
            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> dramaBooks = mongoData.Connect<Books>("Drama");
            List<Books> mysteryBooks = mongoData.Connect<Books>("Mystery");


          
            int row = addInfo(horrorBooks, 0);
            row = addInfo(fantasyBooks, row);
            row = addInfo(dramaBooks, row);
            row = addInfo(mysteryBooks, row);
            ReturnBookBtn(row);
        }


    }
}




          

       