using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Library_assessment.Mongo_Info
{
    internal class MongoData
    {
        const string connectionUri = "mongodb+srv://Isak:juju4u4udahdah5@cluster0.akdrimv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        public List<Members> members;

        public MongoData() { }

        private IMongoDatabase GetMongoDatabase()
        {
           var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client.GetDatabase("Library");
        }

        public void MembersConnect() {
            var dataBase = GetMongoDatabase();
            IMongoCollection<Members> collection = dataBase.GetCollection<Members>("Members");
            members = collection.AsQueryable().ToList<Members>();
        }

    }
}
