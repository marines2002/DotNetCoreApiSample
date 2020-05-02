using System.Collections.Generic;
using ClientApi.Model;
using ClientApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _clientRepository.GetAllClients();
        }
        
        [HttpGet("{id}", Name = "Get")]
        public Client Get(string id)
        {
            return _clientRepository.GetClientById(id);
        }
        
        [HttpPost]
        public void Post([FromBody] Client client)
        {
            _clientRepository.AddClient(client);
        }
    }
}
