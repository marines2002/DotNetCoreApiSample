using System.Collections.Generic;
using ClientApi.Model;
using ClientApi3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientApi3.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
