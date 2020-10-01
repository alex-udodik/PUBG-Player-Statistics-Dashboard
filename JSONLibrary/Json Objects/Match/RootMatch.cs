using JSONLibrary.Json_Objects.AccountID;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class RootMatch
    {
        public Data data { get; set; }

        [JsonProperty(PropertyName = "included")]

        public List<MatchSingleObject> included { get; set; }

        public Links links { get; set; }
        public object meta { get; set; }

    }
}
