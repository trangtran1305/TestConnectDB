using Assignment06.API_Core;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Assignment06.reporter
{
    class HtmlReporter
    {
        private static ExtentReports _report;
        private static  Dictionary<string, ExtentTest> extentTests = new Dictionary<string, ExtentTest>();
        private static string testcaseName;

        public static ExtentReports CreateReport(string path)
        {
            if (_report == null)
            {
                _report = CreateInstance(path);
            }
            return _report;
        }

        private static ExtentReports CreateInstance(string path)
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.DocumentTitle = path;
            htmlReporter.Config.ReportName = path;
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.Encoding = "UTF-8";

            ExtentReports report = new ExtentReports();
            report.AttachReporter(htmlReporter);
            return report;
        }

        public static void flush()
        {
            if (_report != null)
            {
                _report.Flush();
            }
        }

        public static ExtentTest CreateTest(string className, string classDescription = "")
        {
            ExtentTest testClass = _report.CreateTest(className, classDescription);
            extentTests.Add("TestClass", testClass);
            return testClass;
        }

        public static ExtentTest CreateNode(string className, string testcase, string description = "")
        {
            string testcaseName1 = "tescase" + RandomNumber();
            ExtentTest testClass = GetParent();
            if (testClass == null)
            {
                testClass = CreateTest(className);
            }

            ExtentTest testCase = testClass.CreateNode(testcase, description);           
            extentTests.Add(testcaseName1, testCase);
            testcaseName = testcaseName1;
            return testCase;
        }

        public static ExtentTest GetParent()
        {
            ExtentTest testClass = extentTests["TestClass"];
            return testClass;
        }

        public static ExtentTest GetNode()
        {
            ExtentTest testCase = extentTests[testcaseName];
            return testCase;
        }

        public static ExtentTest GetTest()
        {
            if (GetNode() == null)
            {
                return GetParent();
            }
            return GetNode();
        }
        public static void Pass(string des)
        {
            GetTest().Pass(MarkupHelper.CreateLabel(des, ExtentColor.Green));                   
            TestContext.Progress.WriteLine(des);
        }

        public static void Fail(string des, Exception ex)
        {
            GetTest().Fail(MarkupHelper.CreateLabel(des, ExtentColor.Red))
                     .Fail(MarkupHelper.CreateCodeBlock(ex.Message));
            TestContext.Progress.WriteLine(des);
        }

        public static void Fail(string des, Exception ex,string path)
        {

            GetTest().Fail(MarkupHelper.CreateLabel(des, ExtentColor.Red))
                     .Fail(MarkupHelper.CreateCodeBlock(ex.Message))
                     .AddScreenCaptureFromPath(path);
            TestContext.Progress.WriteLine(des);           
        }
       
        public static void Warning(string des)
        {
            
            GetTest().Warning(des);
            TestContext.Progress.WriteLine(des);
        }

        public static void Skip(string des)
        {
            GetTest().Skip(des);
            TestContext.Progress.WriteLine(des);
        }
        public static void Info(RestRequest request, IRestResponse response, string parameter, string header, string requestBody)
        {
            GetTest().Info(MarkupHelperExtra.CreateRequest(request, response, parameter, header, requestBody));

        }
        public static int RandomNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 10000);
        }

    }
}
