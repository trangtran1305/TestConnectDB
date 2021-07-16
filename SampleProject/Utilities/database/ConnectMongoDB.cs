using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using SampleProject.Model;

namespace SampleProject.Utilities
{
    class ConnectMongoDB
    {
        public static async void SaveData(string databaseName, string colectionName, string jsonData)
        {
            //Using MongoClient to connect to Server
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            //Using GetDatabase command to connect Database
            IMongoDatabase database = client.GetDatabase(databaseName);
            //Calling the GetCollection function to retrieve the data table
            IMongoCollection<RootObject> collection = database.GetCollection<RootObject>(colectionName);
            //jsonString = jsonString.Substring(1, jsonString.Length - 2);
            RootObject document = JsonConvert.DeserializeObject<RootObject>(jsonData);
            await collection.InsertOneAsync(document);
                       
        }
        public static List<BsonDocument> ExtractAllData(string databaseName, string colectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");           
            IMongoDatabase database = client.GetDatabase(databaseName);           
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(colectionName);           
            var documents = collection.Find(new BsonDocument()).ToList();           
            foreach (BsonDocument doc in documents)
            {
                Console.WriteLine(doc.ToString());               
            }
            return documents;            
        }
    }
}
