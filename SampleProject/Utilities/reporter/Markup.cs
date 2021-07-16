using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;

namespace Assignment06.API_Core
{
    class Markup : IMarkup
    {
        public RestRequest request { set; get; }
        public IRestResponse response { set; get; }
        public string parameter { set; get;}
        public string header { set; get; }
        public string requestBody { set; get; }


        private int _id { get; } = Interlocked.Increment(ref _cntr);
        private static int _cntr;
        public string GetMarkup()
        {

            return
                "<label style='background-color: green; color: white; padding: 3px; text-align: center; '>" + request.Method.ToString() + "</label>" + " "
                + "<a href= " + "'" + response.ResponseUri + "'" + ">" + response.ResponseUri + "</a>"
                + "<br>"
                + "<label style='color: red'>" + "Parameters:" + " </label>"
                + parameter
                +"<br>"
                + "<label style='color: red'>" + "Headers:" + " </label>"
                + header
                + "<br>"
                + "<label style='color: red'>" + "Request Body:" + " </label>"
                + requestBody
                + "<br>"
                + "<label style='color: red'>" + "Response Content:" + " </label>"
                + "<div class='json-tree' id='code-block-json-" + _id + "'>" 
                +"</div>" +
                    "<script>" +
                    "function jsonTreeCreate" + _id + "() { document.getElementById('code-block-json-" + _id + "').innerHTML = JSONTree.create(" + response.Content + "); }" +
                    "jsonTreeCreate" + _id + "();" +
                    "</script>"
                + "<label style='color: red'>" + "Response Status: " + " </label>" + response.StatusCode;
                    
        }
    }
}
