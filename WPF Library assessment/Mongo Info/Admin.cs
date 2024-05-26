using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Library_assessment.Mongo_Info
{
    internal class Admin
    {
            [BsonId]
            protected ObjectId Id { get; set; }

            [BsonElement("First Name")]

            public string FirstName { get; set; }

            [BsonElement("Last Name")]

            public string LastName { get; set; }

            [BsonElement("Username")]

            public string Username { get; set; }

            [BsonElement("Password")]

            public string Password { get; set; }



            public Admin() { }
        public Admin(string firstName, string lastName, string username, string password ) { }
    }
}
