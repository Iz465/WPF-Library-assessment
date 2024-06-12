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
        public string ImageSource { get; set; }

        [BsonElement("Available")]
        public string Available { get; set; }

        [BsonElement("Overdue")]
        public string Overdue { get; set; }

        [BsonElement("Time")]
        public int Time { get; set; }

        [BsonElement("Genre")]
        public string Genre { get; set; }

        [BsonElement("Owner")]
        public string Owner { get; set; }

     //   [BsonElement("Prebook Owner")]
    //    public string PrebookOwner { get; set; }

        Books() { }
        Books(string name, string author, int pages, string imageSource, string available, string overdue, int time, string genre, string owner) {
            Name = name;
            Author = author;
            Pages = pages;
            Author = author;
            ImageSource = imageSource;
            Available = available;
            Overdue = overdue;
            Time = time;
            Genre = genre;
            Owner = owner;
         //   PrebookOwner = prebookOwner;
        }

    }
}
