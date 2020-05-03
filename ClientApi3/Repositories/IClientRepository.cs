using System.Collections.Generic;
using ClientApi.Model;

namespace ClientApi3.Repositories
{
    public interface IClientRepository
    {
        Client GetClientById(string projectId);
        List<Client> GetAllClients();
        void AddClient(Client client);
    }
}