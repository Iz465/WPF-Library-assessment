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
        private Button submitBtn;
        private MongoData mongoData;

        public UpdateMemberWN()
        {
            InitializeComponent();
            mongoData = new MongoData();
        }

        public void changeMember(Members member)
        {

            firstnameUC = createText(member.FirstName);
            lastnameUC = createText(member.LastName);
            ageUC = createText(member.Age);
            phoneUC = createText(member.PhoneNumber);
            addressUC = createText(member.Address);
            usernameUC = createText(member.Username);
            passwordUC = createText(member.Password);

            submitBtn = createButton("Update");
            submitBtn.Click += (sender, e) => submitBtnClick(sender, e, member);

            int row = 2;

            row = addToGrid(firstnameUC, row, updatingMemberGrid);
            row = addToGrid(lastnameUC, row, updatingMemberGrid);
            row = addToGrid(ageUC, row, updatingMemberGrid);
            row = addToGrid(phoneUC, row, updatingMemberGrid);
            row = addToGrid(addressUC, row, updatingMemberGrid);
            row = addToGrid(usernameUC, row, updatingMemberGrid);
            row = addToGrid(passwordUC, row, updatingMemberGrid);
            Grid.SetRow(submitBtn, row);
            updatingMemberGrid.Children.Add(submitBtn);


        }

        public textboxUC createText(string updateText)
        {
            return new textboxUC
            {
                Text = updateText,
                Width = 200,
                Padding = new Thickness(10),
                Height = 60,
                HorizontalAlignment = HorizontalAlignment.Center
            };

        }
        public Button createButton(string buttonText)
        {
            return new Button
            {
                Content = buttonText,
                Width = 200,
                Padding = new Thickness(10),
                Height = 60,
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }



        public int addToGrid(textboxUC UC, int row, Grid gridname)
        {
            Grid.SetRow(UC, row);
            gridname.Children.Add(UC);
            row++;
            return row;
        }




        void submitBtnClick(object sender, EventArgs e, Members member)
        {
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
