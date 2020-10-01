using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class ParticipantObjectAttributes
    {
        [JsonProperty(PropertyName = "stats")]
        public ParticipantObjectStats stats { get; set; }

        [JsonProperty(PropertyName = "actor")]
        public string actor { get; set; }

        [JsonProperty(PropertyName = "shardId")]
        public string shardId { get; set; }
    }
}
