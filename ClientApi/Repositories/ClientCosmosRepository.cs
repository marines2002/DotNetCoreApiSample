using System.Collections.Generic;
using ClientApi.Builders;
using ClientApi.Model;
using Cosmonaut;
using Cosmonaut.Extensions;

namespace ClientApi.Repositories
{
    public class ClientCosmosRepository : IClientRepository
    {
        //private static readonly string Endpoint = "https://localhost:8081";
        //private static readonly string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private static readonly string Endpoint = "https://nigelcharlton.documents.azure.com:443";
        private static readonly string Key = "pjmGouzXEpq1Ou52dpZJ0ZV23kgINPHquhvdOjh5fay9HnQQvE3ErIHrn6qj2jkTPCB54uHpcD1VzyzmrVbGZw==";

        private static readonly string DatabaseId = "ToDoListCosmonaut";
        private static ICosmosStore<ClientDto> _clientStore;

        public ClientCosmosRepository()
        {
            var cosmosSettings = new CosmosStoreSettings(DatabaseId, Endpoint, Key);

            _clientStore = new CosmosStore<ClientDto>(cosmosSettings);

            _clientStore.AddAsync(new ClientDtoBuilder().WithName("GSK").WithProjectId("GSK001").Build()).GetAwaiter();
            _clientStore.AddAsync(new ClientDtoBuilder().WithName("LBG").WithProjectId("LBG001").Build()).GetAwaiter();
        }

        public void AddClient(Client client)
        {
            var clientDto = new ClientDtoBuilder().WithName(client.Name)
                                                  .WithProjectId(client.ProjectId)
                                                  .WithProjectId(client.StartDate)
                                                  .WithProjectId(client.EndDate)
                                                  .Build();

            _clientStore.AddAsync(clientDto).GetAwaiter();
        }

        public Client GetClientById(string projectId)
        {
            var clientDto = _clientStore.Query().FirstOrDefaultAsync(n => n.ProjectId == projectId);
            
            var client = new Client
            {
                Name = clientDto.Result.Name,
                ProjectId = clientDto.Result.ProjectId,
                StartDate = clientDto.Result.StartDate,
                EndDate = clientDto.Result.EndDate
            };

            return client;
        }

        public List<Client> GetAllClients()
        {
            var clientList = new List<Client>();

            var clientsDto =  _clientStore.Query().ToListAsync().GetAwaiter();

            foreach (var clientDto in clientsDto.GetResult())
            {
                var client = new Client
                {
                    Name = clientDto.Name,
                    ProjectId = clientDto.ProjectId,
                    StartDate = clientDto.StartDate,
                    EndDate = clientDto.EndDate
                };

                clientList.Add(client);
            }

            return clientList;
        }
    }
}
