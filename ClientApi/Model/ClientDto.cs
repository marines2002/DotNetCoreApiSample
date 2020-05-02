using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClientApi.Model
{
    public class ClientDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        //[JsonProperty(PropertyName = "cosmosEntityName")]
        //public string CosmosEntityName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public string StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public string EndDate { get; set; }
    }
}
