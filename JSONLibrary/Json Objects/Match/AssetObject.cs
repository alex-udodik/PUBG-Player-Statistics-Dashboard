using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class AssetObject : MatchSingleObject
    {
        public AssetObjectAttributes attributes { get; set; }

        public override string type { get; } = "asset";

        public string id { get; set; }
    }
}
