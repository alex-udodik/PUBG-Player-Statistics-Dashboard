using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class ParticipantObject : MatchSingleObject
    {
        [JsonProperty(PropertyName = "attributes")]
        public ParticipantObjectAttributes attributes { get; set; }


        public override string type { get; } = "participant";
        public string id { get; set; }
    }
}
