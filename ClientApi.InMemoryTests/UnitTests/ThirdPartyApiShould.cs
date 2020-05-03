using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ClientApi.InMemoryTests.UnitTests
{
    public class ThirdPartyApiShould
    {
        private static WireMockServer _mockServer;
        private static WireMockCaller _simpleCaller;
        private static bool _isStarted;

        [SetUp]
        public void Setup()
        {
            if (_isStarted) return;

            _mockServer = WireMockServer.Start();
            _simpleCaller = new WireMockCaller(_mockServer.Urls.First());
            _isStarted = true;
        }

        [Test]
        public void TestSimpleLoginWireMock()
        {
            var userId = Guid.NewGuid().ToString();
            StubLogin(userId, 42);

            var response = _simpleCaller.DoSimpleLogin(userId);

            response.userName.Should().Be(userId);
            response.id.Should().Be(42);

            var logEntries = _mockServer.LogEntries;

            var logEntry = logEntries.FirstOrDefault(r => r.RequestMessage.Url == $"{_mockServer.Urls.First()}/player/startSession");

            var loginResponse = JsonConvert.DeserializeObject<SimpleLoginRequest>(logEntry.RequestMessage.BodyAsJson.ToString());

            //loginResponse.userName.Should().Be(userId);
        }

        [Test]
        public void TestSimpleLoginWireMockMultiple()
        {
            var rand = new Random();
            for (var i = 1; i <= 5; i++)
            {
                var user = "testUser" + i;
                StubLogin(user, i * 33);
            }

            for (var x = 0; x < 10; x++)
            {
                var testNum = rand.Next() % 5;
                var testUser = "testUser" + (testNum + 1);
                var response = _simpleCaller.DoSimpleLogin(testUser);
                response.userName.Should().Be(testUser);
                response.id.Should().Be((testNum + 1) * 33);
            }

        }

        [Test]
        public void ReturnFailureWhenLoginFails()
        {
            _mockServer
                .Given(Request
                    .Create().WithPath("/player/startSession")
                    .WithBody(new JsonMatcher("{ \"userName\": \"BADWOLF\" }"))
                    .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.Forbidden)
                );


            var response = _simpleCaller.DoSimpleLogin("BADWOLF");
            response.error.Should().Be("Forbidden User");

        }

        private static void StubLogin(string testUser, int expectedId)
        {
            var response = new SimpleLoginResponse
            {
                userName = testUser,
                id = expectedId
            };

            _mockServer
                .Given(Request
                    .Create().WithPath("/player/startSession")
                    .WithBody(new JsonMatcher("{ \"userName\": \"" + testUser + "\" }"))
                    .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(response)
                );

        }
    }
}