using System.Net;
using ClientApi.Builders;
using ClientApi.InMemoryTests.AcceptanceTests.Facade;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace ClientApi.InMemoryTests.AcceptanceTests.Steps
{
    [Binding]
    public class ApiFeatureSteps
    {
        private readonly EcsdApiFacade _facade;

        public ApiFeatureSteps(EcsdApiFacade facade)
        {
             _facade = facade;
        }

        [Given(@"I am using TestServer")]
        public void GivenIAmUsingTestServer()
        {
            _facade.BuildTestServer();
        }

        [When(@"I call valid Values get endpoint")]
        public void WhenICallValidValuesGetEndpoint()
        {
            _facade.CallGetClientsEndpoint("/api/Values").GetAwaiter();
        }

        [When(@"I call valid HealthCheck endpoint")]
        public void WhenICallValidHealthCheckEndpoint()
        {
            _facade.CallHealthCheckEndpoint().GetAwaiter();
        }

        [When(@"I call valid Client get endpoint")]
        public void WhenICallValidClientGetEndpoint()
        {
            _facade.CallGetClientsEndpoint("/api/client").GetAwaiter();
        }

        [When(@"I call valid Client get endpoint with projectId '(.*)'")]
        public void WhenICallValidClientGetEndpointWithProjectId(string projectId)
        {
            _facade.CallGetClientEndpoint($"/api/client/{projectId}").GetAwaiter();
        }

        [Then(@"the correct result is returned")]
        public void ThenTheCorrectResultIsReturned()
        {
           _facade.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the correct response code '(.*)' is returned")]
        public void ThenTheCorrectResponseCodeIsReturned(string code)
        {
            _facade.Response.StatusCode.ToString().Should().Be(code);
        }

        [Then(@"the number of results returned is '(.*)'")]
        public void ThenTheNumberOfResultsReturnedIs(int numberOfRecords)
        {
            _facade.EscdClients.Count.Should().BeGreaterThan(numberOfRecords);
        }

        [Then(@"Client name is '(.*)'")]
        public void ThenClientNameIs(string name)
        {
            _facade.EscdClient.Name.Should().Be(name);
        }

        [Given(@"Some Test Clients Exist")]
        public void GivenSomeTestClientExist()
        {
            var client = new ClientBuilder().Build();
            
            _facade.CallPostClientEndpoint(client).GetAwaiter();
        }

        [Given(@"I call valid Client post endpoint with client with project id '(.*)'")]
        [When(@"I call valid Client post endpoint with client with project id '(.*)'")]
        public void WhenICallValidClientPostEndpointWithClientWithProjectId(string projectId)
        {
            var client = new ClientBuilder().WithProjectId(projectId).Build();

            _facade.CallPostClientEndpoint(client).GetAwaiter();
        }
    }
}
