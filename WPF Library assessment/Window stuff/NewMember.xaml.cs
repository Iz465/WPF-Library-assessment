using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Window_stuff
{
 
    public partial class NewMember : Window
    {
        public NewMember()
        {
            InitializeComponent();
            NewBook newBook = new NewBook();
            textboxUC firstnameUC = newBook.makeText("First Name");
            textboxUC lastnameUC = newBook.makeText("Last Name");
            textboxUC ageUC = newBook.makeText("Age");
            textboxUC phoneUC = newBook.makeText("Phone Number");
            textboxUC addressUC = newBook.makeText("Address");
            textboxUC usernameUC = newBook.makeText("Username");
            textboxUC passwordUC = newBook.makeText("Password");
            Button addBookBtn = new Button(); addBookBtn.Height = 70; addBookBtn.Width = 200; addBookBtn.Content = "Submit"; addBookBtn.Click += (sender, e) =>
            submitmemBtn(firstnameUC.Text, lastnameUC.Text, ageUC.Text, phoneUC.Text, addressUC.Text, usernameUC.Text, passwordUC.Text);
            newBook.setGrid(firstnameUC, 2, newMemGrid);
            newBook.setGrid(lastnameUC, 3, newMemGrid);
            newBook.setGrid(ageUC, 4, newMemGrid);
            newBook.setGrid(phoneUC, 5, newMemGrid);
            newBook.setGrid(addressUC, 6, newMemGrid);
            newBook.setGrid(usernameUC, 7, newMemGrid);
            newBook.setGrid(passwordUC, 8, newMemGrid);
            Grid.SetRow(addBookBtn, 9);
            newMemGrid.Children.Add(addBookBtn);



        }
        void submitmemBtn(string firstname, string lastname, string age, string phone, string address, string username, string password)
        {
            MongoData mongoData = new MongoData();
            var dataBase = mongoData.GetMongoDatabase();
            IMongoCollection<BsonDocument> collection = dataBase.GetCollection<BsonDocument>("Members");

            var newBook = new BsonDocument
            {
                {"First Name", firstname},
                {"Last Name", lastname},
                {"Age", age},
                {"Phone Number", phone},
                {"Address", address },
                {"Username", username },
                {"Password", password }
              
            };
            collection.InsertOne(newBook);
            MessageBox.Show("Member has been added to the library");
            this.Close();

        }

        private void newmemCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
