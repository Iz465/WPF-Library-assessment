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

    public partial class UpdateMemberWN : Window
    {
        private textboxUC firstnameUC;
        private textboxUC lastnameUC;
        private textboxUC ageUC;
        private textboxUC phoneUC;
        private textboxUC addressUC;
        private textboxUC usernameUC;
        private textboxUC passwordUC;
        public UpdateMemberWN()
        {
            InitializeComponent();
        }

        public void changeMember(Members member)
        {
             firstnameUC = new textboxUC(); firstnameUC.Text = member.FirstName; createText(firstnameUC);
             lastnameUC = new textboxUC(); lastnameUC.Text = member.LastName; createText(lastnameUC);
             ageUC = new textboxUC(); ageUC.Text = member.Age; createText(ageUC);
             phoneUC = new textboxUC(); phoneUC.Text = member.PhoneNumber; createText(phoneUC);
             addressUC = new textboxUC(); addressUC.Text = member.Address; createText(addressUC);
             usernameUC = new textboxUC(); usernameUC.Text = member.Username; createText(usernameUC);
             passwordUC = new textboxUC(); passwordUC.Text = member.Password; createText(passwordUC);
            Button submitBtn = new Button(); submitBtn.Content = "Finish Update"; submitBtn.Width = 200; submitBtn.Height = 60; submitBtn.HorizontalAlignment = HorizontalAlignment.Center;
            submitBtn.Click += (sender, e) => submitBtnClick(sender,e, member);

            int row = 2;
            row = addToGrid(firstnameUC, row);
            row = addToGrid(lastnameUC, row);
            row = addToGrid(ageUC, row);
            row = addToGrid(phoneUC, row);
            row = addToGrid(addressUC, row);
            row = addToGrid(usernameUC, row);
            row = addToGrid(passwordUC, row);
               Grid.SetRow(submitBtn, row);
               updatingMemberGrid.Children.Add(submitBtn);


        }

        public void createText(textboxUC UC)
        {
            UC.Width = 200; UC.Padding = new Thickness(10); UC.Height = 60; UC.HorizontalAlignment = HorizontalAlignment.Center;
        }

        public int addToGrid(textboxUC UC, int row)
        {
            Grid.SetRow(UC, row);
            updatingMemberGrid.Children.Add(UC);
            row++;
            return row;
        }

        void submitBtnClick(object sender, EventArgs e, Members member)
        {
            MongoData mongoData = new MongoData();
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Members> collection = database.GetCollection<Members>("Members");
            
                var filter = Builders<Members>.Filter.Eq("_id", member.Id);
                var update = Builders<Members>.Update
                    .Set("First Name", firstnameUC.Text)
                    .Set("Last Name", lastnameUC.Text)
                    .Set("Age", ageUC.Text)
                    .Set("Phone Number", phoneUC.Text)
                    .Set("Address", addressUC.Text)
                    .Set("Username", usernameUC.Text)
                    .Set("Password", passwordUC.Text);

               collection.UpdateOne(filter, update);
            MessageBox.Show("Member has been updated!");
            this.Close();

        }

        private void updateMemCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
