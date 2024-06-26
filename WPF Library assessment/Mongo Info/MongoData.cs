﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPF_Library_assessment.Mongo_Info
{
    public class MongoData
    {
        const string connectionUri = "mongodb+srv://Isak:juju4u4udahdah5@cluster0.akdrimv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

    

        public MongoData() { }

        public IMongoDatabase GetMongoDatabase()
        {
           var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);   
            return client.GetDatabase("Library");
      
        }

        public List<T> Connect<T>(string name)
        {
            var dataBase = GetMongoDatabase();
            IMongoCollection<T> collection = dataBase.GetCollection<T>(name);
            return collection.AsQueryable().ToList();
           
        }


        public void DeleteCollection<T>(string collectionName, string Id)
    {
        var dataBase = GetMongoDatabase();


        IMongoCollection<T> collection = dataBase.GetCollection<T>(collectionName);

        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(Id)); 

        collection.DeleteOne(filter);
    }


        public List<T> SearchBooks<T>(string collectionName, string searchQuery)
        {
            var database = GetMongoDatabase();
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);

            
            var filter = Builders<T>.Filter.Text(searchQuery);

            var searchResults = collection.Find(filter).ToList();

            return searchResults;
        }

        public static class BookReturnNotifier
        {
            public static event Action BooksReturned;

            public static void NotifyBooksReturned()
            {
                BooksReturned.Invoke();
            }
        }

        public List<string> getCollections()
        {
            return new List<string>{"Horror", "Fantasy", "Sci-Fi", "Romance", "Mystery", "History"};
        }
        

        public interface reloadPage
        {
            void resetPage();
        }
       


    }
}
