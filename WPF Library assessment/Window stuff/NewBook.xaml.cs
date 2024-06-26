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

            textboxUC nameText = makeText("Name");
            textboxUC authorText = makeText("Author");
            textboxUC PagesText = makeText("Pages");
            textboxUC collectionText = makeText("Collection");
            Button addBookBtn = new Button(); addBookBtn.Height = 70; addBookBtn.Width = 200; addBookBtn.Content = "Submit"; addBookBtn.Click += (sender, e) =>
            submitBtn(nameText.Text, authorText.Text, PagesText.Text, collectionText.Text);
            setGrid(nameText, 2, newGrid);
            setGrid(authorText,3, newGrid);
            setGrid(PagesText, 4, newGrid);
            setGrid(collectionText, 5, newGrid);
            Grid.SetRow(addBookBtn, 6);
            newGrid.Children.Add(addBookBtn);
        
        }

      public textboxUC makeText(string placeholder)
        {
            return new textboxUC
            {
                Height = 70,
                Width = 200,
                PlaceHolder = placeholder
            };
        }

     public void setGrid(textboxUC UC, int row, Grid gridname)
        {
           Grid.SetRow(UC,row);
            gridname.Children.Add(UC);

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
            this.Close();

        }




    }
}
