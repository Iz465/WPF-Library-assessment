using MongoDB.Bson.Serialization.Attributes;
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

namespace WPF_Library_assessment.Window_stuff
{

    public partial class SignUpWN : Window
    {
        public SignUpWN()
        {
            InitializeComponent();

        }


        private void closeSignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (firstnametext.Text == string.Empty || lastnametext.Text == string.Empty || agetext.Text == string.Empty
                || phonetext.Text == string.Empty || addresstext.Text == string.Empty || usernametext.Text == string.Empty || passwordtext.Text == string.Empty) 
            {
                MessageBox.Show("Please fill in every box");
                return;
            }

            if (passwordtext.Text != redoPasswordtext.Text)
            {
                MessageBox.Show("Passwords not matching");
                return;
            }

            else
            {
                MongoData mongoData = new MongoData();
                var database = mongoData.GetMongoDatabase();
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Members");

                var newMember = new BsonDocument
            {
                {"First Name", firstnametext.Text},
                {"Last Name", lastnametext.Text},
                {"Age", agetext.Text},
                {"Phone Number", phonetext.Text},
                {"Address", addresstext.Text},
                {"Username", usernametext.Text},
                {"Password", passwordtext.Text}

            };
                collection.InsertOne(newMember);
                MessageBox.Show($"Congratulations {firstnametext.Text}. You are now a member of the library");
                this.Close();
            
            
            }

           
      
        }
    }
}


