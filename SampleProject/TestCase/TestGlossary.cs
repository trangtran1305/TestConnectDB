using Assignment06.API_Core;
using Assignment06.reporter;
using NUnit.Framework;
using System;
using SampleProject.Utilities;
using SampleProject.Services;
using SampleProject.Model;
using System.Threading;
using System.IO;
using Assignment06.Test_Setup;
using SampleProject.Utilities.database;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace Assignment06
{
    public class TestGlossary
    {              
        GlossaryService glossaryService;
        string postGlossaryData;
        String datasource = @"(LocalDb)\MSSQLLocalDB";
        String database = "Sample";
        String colection = "Glossary";
        

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Common.InitReportDirection();
            HtmlReporter.CreateReport(Common.REPORT_HTML_FILE);
            HtmlReporter.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void beforeTest()
        {
            HtmlReporter.CreateNode(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.MethodName);
            glossaryService = new GlossaryService();
            StreamReader r = new StreamReader(Constant.JSON_FILE);
            string json = r.ReadToEnd();
            postGlossaryData = glossaryService.PostGlossaryData(json);
        }

        [Test]
        public void Test01_VerifyDataInMongoDB()
        {               
            ConnectMongoDB.SaveData(database, colection, postGlossaryData);
            Thread.Sleep(3000);
            List<BsonDocument> documents = ConnectMongoDB.ExtractAllData(database, colection);            
            List<RootObject> glossaries = new List<RootObject>();
            foreach (BsonDocument doc in documents)
            {              
                glossaries.Add(BsonSerializer.Deserialize<RootObject>(doc));
            }
            string ActualID = glossaries[glossaries.Count -1].glossary.GlossDiv.GlossList.GlossEntry.ID;
            Assert.AreEqual(ActualID,"SGML");
           
        }
        [Test]
        public void Test02_VerifyDataInSqlServer()
        {
            string insert_sql = "Insert Into[Sample].[dbo].[Glossary](glossary)Values( '" + postGlossaryData + "')";
            ConnectSqlServer.InsertData(datasource, database, insert_sql);
            string query_sql = "SELECT[glossary] FROM[Sample].[dbo].[Glossary]";
            List<string> extractData = ConnectSqlServer.ExtractAllData(datasource, database, query_sql);
            List<RootObject> glossaries = new List<RootObject>();
            foreach (string data in extractData)
            {
                glossaries.Add(JsonConvert.DeserializeObject <RootObject>(data));               
            }
            string ActualID = glossaries[glossaries.Count - 1].glossary.GlossDiv.GlossList.GlossEntry.ID;
            Assert.AreEqual(ActualID, "SGML");

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            HtmlReporter.flush();
        }
        

    }
}