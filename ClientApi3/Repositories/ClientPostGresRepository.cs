using System.Collections.Generic;
using System.Linq;
using ClientApi.Model;
using ClientApi3.Builders;
using ClientApi3.Model;

namespace ClientApi3.Repositories
{
    public class ClientPostGresRepository : IClientRepository
    {
        private readonly List<ClientDto> _clientList;

        public ClientPostGresRepository()
        {
            _clientList = new List<ClientDto>
            {
                new ClientDtoBuilder().WithName("GSK").WithProjectId("GSK001").Build(),
                new ClientDtoBuilder().WithName("GSK").WithProjectId("GSK002").Build(),
                new ClientDtoBuilder().WithName("LBG").WithProjectId("LBG001").Build(),
                new ClientDtoBuilder().WithName("LBG").WithProjectId("LBG002").Build(),
                new ClientDtoBuilder().WithName("GSK").WithProjectId("GSK003").Build(),
            };
        }

        public void AddClient(Client client)
        {
            var clientDto = new ClientDtoBuilder().WithName(client.Name)
                .WithProjectId(client.ProjectId)
                .WithProjectId(client.StartDate)
                .WithProjectId(client.EndDate)
                .Build();

            _clientList.Add(clientDto);
        }

        public Client GetClientById(string projectId)
        {
            var clientDto = _clientList.FirstOrDefault(n => n.ProjectId == projectId);

            var client = new Client
            {
                Name = clientDto.Name,
                ProjectId = clientDto.ProjectId,
                StartDate = clientDto.StartDate,
                EndDate = clientDto.EndDate
            };

            return client;
        }

        public List<Client> GetAllClients()
        {
            var clientList = new List<Client>();

            foreach (var clientDto in _clientList)
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
