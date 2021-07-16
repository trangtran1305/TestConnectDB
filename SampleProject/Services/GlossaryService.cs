using Assigment06.API_Core;
using Newtonsoft.Json;
using SampleProject.Model;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Constant = Assignment06.Test_Setup.Constant;

namespace SampleProject.Services
{
    class GlossaryService
    {
        
        public string getGlossaryDataPath = "/glossary";
        public string postGlossaryDataPath = "/glossary";


        //POST
        public Response PostGlossaryRequest(string jsonString)
        {
            Response response = new Request().SetUrl(Constant.API_HOST_SEVICE + getGlossaryDataPath)
                                             .SetPost()                                             
                                             .AddHeader("Accept", "application/json")
                                             .SetJsonBody(jsonString)
                                             .ExecuteRequest();
            return response;
        }
        public string PostGlossaryData(string jsonString)
        {
            var postGlossaryData = PostGlossaryRequest(jsonString).GetResponseBody();
            return postGlossaryData;
        }
        //GET
        public Response GetGlossaryRequest()
        {
            Response response = new Request().SetUrl(Constant.API_HOST_SEVICE + getGlossaryDataPath)
                                             .SetGet()                                            
                                             .AddHeader("Accept", "application/json")
                                             .ExecuteRequest();
            return response;
        }
        public string GetGlossaryData()
        {
            var getGlossaryData = GetGlossaryRequest().GetResponseBody();
            return getGlossaryData;
        }

    }
}

