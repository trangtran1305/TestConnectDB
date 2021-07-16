using Assignment06.reporter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace Assigment06.API_Core
{
    class Request
    {       
        private RestRequest request;
        private string url;
        string parameter;
        string header;
        string requestBody;

        public Request SetUrl(string url)
        {
            this.url = url;
            return this;
        }
        public Request SetPathVariable(string pathVariable)
        {
            this.url = url + pathVariable;
            return this;
        }

        public Request AddHeader(string key,string value)
        {
            request.AddHeader(key, value);
            request.RequestFormat = DataFormat.None;
            header = key + ":" + value;
            return this;
        }
        public Request AddParameter(string key, string value)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + key + "=" + value;
                
            }
            else
            {
                url = url + "?" + key + "=" + value;              
            }
            //request.AddParameter(key, value);
            parameter = key + ":" + value + " ";
            return this;
        }
      
        public Request SetJsonBody(string jsonString )
        {
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(jsonString);
                requestBody = jsonString;
                return this;
           
        }
        public Request SetJsonBody(JObject jObjectbody)
        {
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            requestBody = JsonConvert.SerializeObject(jObjectbody);
            return this;

        }
        public Request SetXmlBody(string xmlString)
        {
            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(xmlString);
            return this;
        }
        public Request SetGet()
        {
          
                request = new RestRequest(Method.GET);
                return this;
        }
        public Request SetPost()
        {           
            request = new RestRequest(Method.POST);          
            return this;
        }

        public Request SetPut()
        {
            request = new RestRequest(Method.PUT);
            return this;
        }
        public Request SetDelete()
        {
            request = new RestRequest(Method.DELETE);
            return this;
        }
        public Response ExecuteRequest()
        {
            try
            {
                RestClient client = new RestClient(url);
                IRestResponse response = client.Execute(request);
                HtmlReporter.Pass("Request successfully!");
                HtmlReporter.Info(request, response, parameter, header, requestBody);
                return new Response(response);
            }
            catch (Exception ex)
            {
                HtmlReporter.Fail("Can't request successfully!",ex);
                throw ex;
            }
        }

    }
}
