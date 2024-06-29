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
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace WPF_Library_assessment.Window_stuff
{

    public partial class UpdateWN : Window
    {
        private textboxUC nameUC;
        private textboxUC authorUC;
        private textboxUC pagesUC;
        private textboxUC collectionUC;
        private Button submitBtn;
        private UpdateMemberWN updateMemberWN;
        private MongoData mongoData;


        public UpdateWN()
        {
            InitializeComponent();
            updateMemberWN = new UpdateMemberWN();
            mongoData = new MongoData();
        }


        public void changeBook(Books book)
        {
            nameUC = updateMemberWN.createText(book.Name);
            authorUC = updateMemberWN.createText(book.Author);
            pagesUC = updateMemberWN.createText(book.Pages.ToString());
            collectionUC = updateMemberWN.createText(book.Genre);
            submitBtn = updateMemberWN.createButton("Update");
            submitBtn.Click += (sender, e) => submitBtnClick(sender, e, book);
            int row = 2;
            row = updateMemberWN.addToGrid(nameUC, row, updateGrid);
            row = updateMemberWN.addToGrid(authorUC, row, updateGrid);
            row = updateMemberWN.addToGrid(pagesUC, row, updateGrid);
            row = updateMemberWN.addToGrid(collectionUC, row, updateGrid);
            Grid.SetRow(submitBtn, row);
            updateGrid.Children.Add(submitBtn);

        }

        void submitBtnClick(object sender, EventArgs e, Books book)
        {
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Members> collection = database.GetCollection<Members>(book.Genre);

            var filter = Builders<Members>.Filter.Eq("_id", book.Id);
            var update = Builders<Members>.Update
                .Set("Name", nameUC.Text)
                .Set("Author", authorUC.Text)
                .Set("Pages", pagesUC.Text);

            collection.UpdateOne(filter, update);
            MessageBox.Show("Member has been updated!");
            this.Close();
        }


        private void updateWnCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }






}
