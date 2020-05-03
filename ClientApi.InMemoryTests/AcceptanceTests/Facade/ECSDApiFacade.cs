using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientApi.Model;
using ClientApi3;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace ClientApi.InMemoryTests.AcceptanceTests.Facade
{
    public class EcsdApiFacade
    {
        public HttpClient Client { get; set; }
        public HttpResponseMessage Response { get; set; }
        public List<Client> EscdClients { get; set; }
        public Client EscdClient { get; set; }

        public void BuildTestServer()
        {
            //this needs to change
            var testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());

            Client = testServer.CreateClient();
        }

        public async Task CallPostClientEndpoint(Client client)
        {
            EscdClient = client;

            var content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");

            Response = await Client.PostAsync("/api/client", content);
        }

        public async Task CallGetClientEndpoint(string endPoint)
        {
            Response = Client.GetAsync(endPoint).Result;

            var customerJsonString = await Response.Content.ReadAsStringAsync();

            EscdClient = JsonConvert.DeserializeObject<Client>(customerJsonString);
        }

        public async Task CallGetClientsEndpoint(string endPoint)
        {
            Response = Client.GetAsync(endPoint).Result;
            
            var customerJsonString = await Response.Content.ReadAsStringAsync();

            EscdClients = JsonConvert.DeserializeObject<List<Client>>(customerJsonString);
        }

        public async Task CallHealthCheckEndpoint()
        {
            Response = Client.GetAsync("api/HealthCheck").Result;

            var customerJsonString = await Response.Content.ReadAsStringAsync();
        }
    }
}
