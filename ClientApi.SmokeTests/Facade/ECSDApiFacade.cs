using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientApi.Model;
using Newtonsoft.Json;

namespace ClientApi.SmokeTests.Facade
{
    public class EcsdApiFacade
    {
        private const string Endpoint = "https://clientapi20190618092952.azurewebsites.net";

        public HttpClient Client { get; set; }
        public HttpResponseMessage Response { get; set; }
        public List<Client> EscdClients { get; set; }

        public void BuildHttpClient()
        {
            Client = new HttpClient { BaseAddress = new Uri(Endpoint) };

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task CallGetClientsEndpoint()
        {
            Response = Client.GetAsync("/api/client").Result;

            var customerJsonString = await Response.Content.ReadAsStringAsync();

            EscdClients = JsonConvert.DeserializeObject<List<Client>>(customerJsonString);
        }
    }
}
