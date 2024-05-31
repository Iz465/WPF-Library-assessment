using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPF_Library_assessment.Mongo_Info
{
    internal class MongoData
    {
        const string connectionUri = "mongodb+srv://Isak:juju4u4udahdah5@cluster0.akdrimv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

    

        public MongoData() { }

        public IMongoDatabase GetMongoDatabase()
        {
           var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);   // dont need to repeat
            return client.GetDatabase("Library");
      
        }

        public List<T> Connect<T>(string name)
        {
            var dataBase = GetMongoDatabase();
            IMongoCollection<T> collection = dataBase.GetCollection<T>(name);
            return collection.AsQueryable().ToList();
            
        }


        public void DeleteCollection<T>(string collectionName, string bookId)
    {
        var dataBase = GetMongoDatabase();
        IMongoCollection<T> collection = dataBase.GetCollection<T>(collectionName);

        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(bookId)); 

        collection.DeleteOne(filter);
    }

    //   public void UpdateCollection<T>(string collectionName, string bookId)
     //   {
      //     var dataBase = GetMongoDatabase();
     //      IMongoCollection<T> collection = dataBase.GetCollection<T>(collectionName);

      //      var filter = Builders<T>.Filter.Eq("_id", new ObjectId(bookId));

           
    //    }



}
}
