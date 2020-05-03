using System;
using ClientApi3.Model;

namespace ClientApi3.Builders
{
    public class ClientDtoBuilder
    {
        private string name = "GSK";
        private string projectId = "GSK001";
        private string startDate = DateTime.Now.AddDays(3).ToShortDateString();
        private string endDate = DateTime.Now.AddDays(10).ToShortDateString();
        private string id;

        public ClientDtoBuilder()
        {
            var random = new Random();

            id = random.Next(1000, 9999).ToString();
        }

        public ClientDtoBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ClientDtoBuilder WithProjectId(string projectId)
        {
            this.projectId = projectId;
            return this;
        }

        public ClientDtoBuilder WithStartDate(string startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public ClientDtoBuilder WithEndDate(string endDate)
        {
            this.endDate = endDate;
            return this;
        }

        public ClientDto Build()
        {
            return new ClientDto
            {
                Name = name,
                ProjectId = projectId,
                StartDate = startDate,
                EndDate = endDate,
                Id = id//,
                //CosmosEntityName = cosmosEntityName
            };
        }
    }
}
