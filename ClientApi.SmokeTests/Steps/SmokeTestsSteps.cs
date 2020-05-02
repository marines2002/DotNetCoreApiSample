using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClientApi.SmokeTests.Facade;
using Shouldly;
//using FluentAssertions;
using TechTalk.SpecFlow;

namespace ClientApi.SmokeTests.Steps
{
    [Binding]
    public class SmokeTestsSteps
    {
        private readonly EcsdApiFacade _facade;

        public SmokeTestsSteps(EcsdApiFacade facade)
        {
            _facade = facade;
        }

        [Given(@"I am using a HttpClient")]
        public void GivenIAmUsingAHttpClientAsync()
        {
            _facade.BuildHttpClient();
        }
        
        [When(@"I call valid Client get endpoint")]
        public void WhenICallValidClientGetEndpoint()
        {
            _facade.CallGetClientsEndpoint().GetAwaiter();
        }
        
        [Then(@"the correct response code '(.*)' is returned")]
        public void ThenTheCorrectResponseCodeIsReturned(string statusCode)
        {
            _facade.Response.StatusCode.ToString().ShouldBe(statusCode);
        }
        
        [Then(@"some clients are returned")]
        public void ThenSomeClientsAreReturned()
        {
            _facade.EscdClients.Count.ShouldBeGreaterThan(0);
        }
    }
}
