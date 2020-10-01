using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Ranked_Objects
{
    public class RankedGameModeStats
    {
        [JsonProperty(PropertyName = "squad")]
        public Squad squad { get; set; }


        [JsonProperty(PropertyName = "squad-fpp")]
        public SquadFPP squadFpp { get; set; }

    }
}
