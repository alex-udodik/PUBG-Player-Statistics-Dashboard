using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class RosterObject : MatchSingleObject
    {
        public RosterObjectAttributes attributes { get; set; }
        public RosterObjectRelationships relationships { get; set; }

        public override string type { get; } = "roster";
        public string id { get; set; }
    }
}
