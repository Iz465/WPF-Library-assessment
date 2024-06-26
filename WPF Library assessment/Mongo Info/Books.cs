﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Windows.Threading;

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
        [BsonElement("Overdue")]
        public string Overdue { get; set; }
        [BsonElement("Time")]
        public int Time { get; set; }

        [BsonElement("Owner")]
        public string Owner { get; set; }
        [BsonElement("PreBookOwner")]
        public string PreBookOwner { get; set; }

        [BsonElement("Genre")]
        public string Genre {  get; set; }





    }
   
}
