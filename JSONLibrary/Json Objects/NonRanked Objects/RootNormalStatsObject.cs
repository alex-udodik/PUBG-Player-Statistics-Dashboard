using JsonApiSerializer.JsonApi;
using JSONLibrary.Json_Objects.Ranked_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Regular_Objects
{
    public class RootNormalStatsObject : StatsObject
    {
        public Data data { get; set; }

        public Ranked_Objects.Links links { get; set; }
        public Ranked_Objects.Meta meta { get; set; }
    }
}
