using JSONLibrary.Json_Objects.AccountID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Leaderboard
{
    public class RootLeaderboard
    {
        public Data date { get; set; }
        public List<Included> included { get; set; }

        public Links links { get; set; }
        public object meta { get; set; }
    }
}
