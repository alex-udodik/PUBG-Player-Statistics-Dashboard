using JsonApiSerializer.JsonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Ranked_Objects
{
    public class RootRankedStatsObject : StatsObject
    {
        public Data data { get; set; }
        public Links links { get; set; }
        public Meta meta { get; set; }
    }
}
