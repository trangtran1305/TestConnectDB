using Assignment06.API_Core;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigment06.API_Core
{
    class Response
    {
        private IRestResponse response;
        
        public Response(IRestResponse response)
        {
            this.response = response;
        }
        public int GetStatusCode()
        {
            return (int)response.StatusCode;
        }
        public string GetResponseStatus()
        {
            return response.ResponseStatus.ToString();
        }
        public string GetResponseBody()
        {
            return response.Content;
        }
        
       

    }
}
