using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WPF_Library_assessment.Mongo_Info
{
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

        public string PhoneNumber  { get; set; }

        [BsonElement("Address")]

        public string Address { get; set; }

        [BsonElement("Username")]

        public string Username { get; set; }

        [BsonElement("Password")]

        public string Password { get; set; }

        public Members(string firstName, string lastName, string age, string phoneNumber, string address,
            string username, string password) {
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
