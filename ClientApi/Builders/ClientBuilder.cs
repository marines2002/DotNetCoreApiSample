using System;
using System.Security.Cryptography.X509Certificates;
using AutoFixture;
using ClientApi.Model;

namespace ClientApi.Builders
{
    public class ClientBuilder
    {
        private readonly Client _client;

        public ClientBuilder()
        {
            var fixture = new Fixture();
            _client = fixture.Create<Client>();
        }

        public ClientBuilder WithName(string name)
        {
            this._client.Name = name;
            return this;
        }

        public ClientBuilder WithProjectId(string projectId)
        {
            this._client.ProjectId = projectId;
            return this;
        }

        public Client Build()
        {
            return _client;
        }
    }
}
