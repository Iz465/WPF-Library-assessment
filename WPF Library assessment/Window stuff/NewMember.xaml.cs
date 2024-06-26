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
            newBook.setGrid(firstnameUC, 2, newMemGrid);
            newBook.setGrid(lastnameUC, 3, newMemGrid);
            newBook.setGrid(ageUC, 4, newMemGrid);
            newBook.setGrid(phoneUC, 5, newMemGrid);
            newBook.setGrid(addressUC, 6, newMemGrid);
            newBook.setGrid(usernameUC, 7, newMemGrid);
            newBook.setGrid(passwordUC, 8, newMemGrid);


        }
        void submitBtn(string name, string author, string pages, string collectionName)
        {
            MongoData mongoData = new MongoData();
            var dataBase = mongoData.GetMongoDatabase();
            IMongoCollection<BsonDocument> collection = dataBase.GetCollection<BsonDocument>(collectionName);

            var newBook = new BsonDocument
            {
                {"Name", name},
                {"Author", author},
                {"Pages", pages},
                {"Available", "Yes" },
                {"Overdue", "No" },
                {"Time", 300 },
                {"Owner", string.Empty },
                {"PreBookOwner", string.Empty },
                {"Image", "https://storage.googleapis.com/librarybookimages/book%20placeholder%20image.avif" }

            };
            collection.InsertOne(newBook);
            MessageBox.Show($"{name} has been added to the library");

        }

    }
}
