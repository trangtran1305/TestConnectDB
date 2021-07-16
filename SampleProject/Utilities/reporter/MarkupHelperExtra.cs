using AventStack.ExtentReports.MarkupUtils;
using RestSharp;

namespace Assignment06.API_Core
{
    class MarkupHelperExtra
    {
        public static IMarkup CreateRequest(RestRequest request, IRestResponse response, string parameter, string header, string requestBody)
        {
            var ApiInfo = new Markup();
            ApiInfo.request = request;
            ApiInfo.response = response;
            ApiInfo.parameter = parameter;
            ApiInfo.header = header;
            ApiInfo.requestBody = requestBody;
            return ApiInfo;
        }
    }
}
