using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WPF_Library_assessment.Mongo_Info
{
    public class Books
    {
        [BsonId] 
        public ObjectId Id { get; set; }

        [BsonElement("Name")]

        public string Name { get; set; }

        [BsonElement("Author")] 

        public string Author { get; set; }

        [BsonElement("Pages")] 

        public int Pages { get; set; }

        [BsonElement("Image")] 
        public string Image { get; set; }

        [BsonElement("Available")]
        public string Available { get; set; }

        Books() { }
        Books(string name, string author, int pages, string image, string available) {
            Name = name;
            Author = author;
            Pages = pages;
            Author = author;
            Image = image;
            Available = available;
        }

    }
}
