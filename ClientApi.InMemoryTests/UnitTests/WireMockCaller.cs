using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace ClientApi.InMemoryTests.UnitTests
{
    public class WireMockCaller
    {
        private readonly string _baseUrl;
        public WireMockCaller(string baseUrl){
            _baseUrl = baseUrl;
        }
        public SimpleLoginResponse DoSimpleLogin(string userName)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("/player/startSession", Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(new SimpleLoginRequest
            {
                userName = userName
            });

            var response = (RestResponse)client.Execute(request);

            SimpleLoginResponse loginResponse = null;


            if (response.StatusCode == HttpStatusCode.OK)
                loginResponse = JsonConvert.DeserializeObject<SimpleLoginResponse>(response.Content);
            else if (response.StatusCode == HttpStatusCode.Forbidden)
                loginResponse = new SimpleLoginResponse
                {
                    error = "Forbidden User"
                };


            return loginResponse;
        }
    }

    public class SimpleLoginRequest
    {
        public string userName;
    }

    public class SimpleLoginResponse
    {
        public int id;
        public string userName;
        public string error;
    }
}
