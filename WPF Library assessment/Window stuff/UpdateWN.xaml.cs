using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

    public partial class UpdateWN : Window
    {
        public UpdateWN()
        {
            InitializeComponent();
        }


        public void changeBook(string collection, string id, string name, string author, string pages, string available)
        {
            

                  textboxUC nameUC = new textboxUC();
                  textboxUC authorUC = new textboxUC();
                  textboxUC pagesUC = new textboxUC();
                  textboxUC availableUC = new textboxUC();
                  Button submitBtn = new Button();

                  nameUC.Text = name; nameUC.Width = 200; nameUC.Padding = new Thickness(10); nameUC.Height = 60; nameUC.HorizontalAlignment= HorizontalAlignment.Center;
                  authorUC.Text = author; authorUC.Width = 200; authorUC.Padding = new Thickness(10); authorUC.Height = 60; authorUC.HorizontalAlignment= HorizontalAlignment.Center; 
                  pagesUC.Text = pages; pagesUC.Width = 200; pagesUC.Padding = new Thickness(10); pagesUC.Height = 60; pagesUC.HorizontalAlignment= HorizontalAlignment.Center; 
                  availableUC.Text = available; availableUC.Padding = new Thickness(10); availableUC.Width = 200; availableUC.Height = 60; availableUC.HorizontalAlignment= HorizontalAlignment.Center; 
                  submitBtn.Content = "Finish Update";  submitBtn.Width = 200; submitBtn.Height = 60; submitBtn.HorizontalAlignment = HorizontalAlignment.Center; submitBtn.Click += (sender, e) => submitBtnClick<Books>(collection, id, nameUC.Text, authorUC.Text, pagesUC.Text, availableUC.Text);

                  Grid.SetRow(nameUC,2);
                  Grid.SetRow(authorUC,3);
                  Grid.SetRow(pagesUC,4);
                  Grid.SetRow(availableUC,5);
                 Grid.SetRow(submitBtn,6);
                  updateGrid.Children.Add(nameUC);
                  updateGrid.Children.Add(authorUC);
                  updateGrid.Children.Add(pagesUC);
                  updateGrid.Children.Add(availableUC);
                  updateGrid.Children.Add(submitBtn); 

              } 
            
                    void submitBtnClick<T>(string collectionName, string id, string name, string author, string pages, string available) {
                    MongoData mongoData = new MongoData();
                        var database = mongoData.GetMongoDatabase();
                        IMongoCollection<T> collection = database.GetCollection<T>(collectionName);

                        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                        var update = Builders<T>.Update
                            .Set("Name", name)
                            .Set("Author", author)
                            .Set("Pages", pages)
                            .Set("Available", available);

                            collection.UpdateMany(filter, update);
                    }
            
        
    }

  




}
