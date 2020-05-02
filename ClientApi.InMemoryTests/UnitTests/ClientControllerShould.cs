using System.Collections.Generic;
using ClientApi.Builders;
using ClientApi.Controllers;
using ClientApi.Model;
using ClientApi.Repositories;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ClientApi.InMemoryTests.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReturnAllClientsWhenGetAllClientsIsCalled()
        {
            var mockClientRepository = new Mock<IClientRepository>();

            var expectedClientList = new List<Client>
            {
                new ClientBuilder().WithName("GSK").WithProjectId("GSK001").Build(),
                new ClientBuilder().WithName("GSK").WithProjectId("GSK002").Build(),
                new ClientBuilder().WithName("LBG").WithProjectId("LBG001").Build(),
                new ClientBuilder().WithName("LBG").WithProjectId("LBG002").Build(),
                new ClientBuilder().WithName("GSK").WithProjectId("GSK003").Build(),
            };

            mockClientRepository.Setup(x => x.GetAllClients()).Returns(expectedClientList);

            var clientController = new ClientController(mockClientRepository.Object);

            var actualClientList = clientController.Get();

            actualClientList.Should().BeEquivalentTo(expectedClientList);
        }

        [Test]
        public void ReturnOneClientsWhenGetClientByIdIsCalled()
        {
            var mockClientRepository = new Mock<IClientRepository>();

            var expectedClient = new ClientBuilder().WithName("GSK").WithProjectId("GSK001").Build();

            mockClientRepository.Setup(x => x.GetClientById(It.IsAny<string>())).Returns(expectedClient);

            var clientController = new ClientController(mockClientRepository.Object);

            var actualClient = clientController.Get("GSK001");

            actualClient.Should().BeEquivalentTo(expectedClient);
        }

        [Test]
        public void CallAddClientClientsWhenGetClientByIdIsCalled()
        {
            var mockClientRepository = new Mock<IClientRepository>();

            var client = new Client();

            mockClientRepository.Setup(mock => mock.AddClient(It.IsAny<Client>()));

            var clientController = new ClientController(mockClientRepository.Object);

            clientController.Post(client);

            mockClientRepository.Verify(mock => mock.AddClient(client), Times.Once());
        }
    }
}