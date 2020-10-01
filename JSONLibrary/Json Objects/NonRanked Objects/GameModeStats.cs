using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Regular_Objects
{
    public class GameModeStats
    {
        [JsonProperty(PropertyName = "duo")]
        public DuoStats duoStats { get; set; }

        [JsonProperty(PropertyName = "duo-fpp")]
        public DuoFPPStats duoFppStats { get; set; }

        [JsonProperty(PropertyName = "solo")]
        public SoloStats soloStats { get; set; }

        [JsonProperty(PropertyName = "solo-fpp")]
        public SoloFPPStats soloFppStats { get; set; }

        [JsonProperty(PropertyName = "squad")]
        public SquadStats squadStats { get; set; }

        [JsonProperty(PropertyName = "squad-fpp")]
        public SquadFPPStats squadFPPStats { get; set; }
    }
}
