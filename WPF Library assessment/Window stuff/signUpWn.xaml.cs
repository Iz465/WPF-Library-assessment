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
using WPF_Library_assessment.Library_Pages;

namespace WPF_Library_assessment.Window_stuff
{
    public partial class signUpWn : Window
    {
        private readonly IMongoCollection<Members> _membersCollection;

        public signUpWn()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb+srv://Isak:juju4u4udahdah5@cluster0.akdrimv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            var database = client.GetDatabase("Library"); 
            _membersCollection = database.GetCollection<Members>("Members");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string age = AgeTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string address = AddressTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(age) ||
                string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                StatusTextBlock.Text = "All fields are required.";
                return;
            }

            if (password != confirmPassword)
            {
                StatusTextBlock.Text = "Passwords do not match.";
                return;
            }

            var newMember = new Members(firstName, lastName, age, phoneNumber, address, email, password);

            // Insert the new member into the MongoDB collection
            _membersCollection.InsertOne(newMember);

            StatusTextBlock.Text = "Sign-up successful!";
        }
    }

    internal class Members
    {
        [BsonId]
        protected ObjectId Id { get; set; }

        [BsonElement("First Name")]
        public string FirstName { get; set; }

        [BsonElement("Last Name")]
        public string LastName { get; set; }

        [BsonElement("Age")]
        public string Age { get; set; }

        [BsonElement("Phone Number")]
        public string PhoneNumber { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        public Members() { }

        public Members(string firstName, string lastName, string age, string phoneNumber, string address,
                       string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            PhoneNumber = phoneNumber;
            Address = address;
            Username = username;
            Password = password;
        }
    }
}