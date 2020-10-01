using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(ParticipantObject), "participant")]
    [JsonSubtypes.KnownSubType(typeof(RosterObject), "roster")]
    [JsonSubtypes.KnownSubType(typeof(AssetObject), "asset")]
    public class MatchSingleObject
    {
        [JsonProperty(PropertyName = "type")]
        public virtual string type { get; }

    }
   
}
