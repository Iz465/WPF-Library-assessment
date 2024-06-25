using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Window_stuff
{

    public partial class NewBook : Window
    {
        public NewBook()
        {
          
            InitializeComponent();

           textboxUC nameText = new textboxUC(); nameText.Height = 70; nameText.Width = 200;
           textboxUC authorText = new textboxUC(); authorText.Height = 70; authorText.Width = 200;
           textboxUC PagesText = new textboxUC(); PagesText.Height = 70; PagesText.Width = 200;
           textboxUC collectionText = new textboxUC(); collectionText.Height = 70; collectionText.Width = 200;
           Button addBookBtn = new Button(); addBookBtn.Height = 70; addBookBtn.Width = 200; addBookBtn.Content = "Submit"; addBookBtn.Click += (sender, e) =>
           submitBtn(nameText.Text, authorText.Text, PagesText.Text, collectionText.Text);
           Grid.SetRow(nameText,1);
            Grid.SetRow(authorText,2);
            Grid.SetRow(PagesText,3);
            Grid.SetRow(collectionText,4);
            Grid.SetRow(addBookBtn,5);
            newGrid.Children.Add(nameText);
            newGrid.Children.Add(authorText);
            newGrid.Children.Add(PagesText);
            newGrid.Children.Add(collectionText);
            newGrid.Children.Add(addBookBtn);
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
                {"Available", "Yes" }
            };
            collection.InsertOne(newBook);
       

        }




    }
}
