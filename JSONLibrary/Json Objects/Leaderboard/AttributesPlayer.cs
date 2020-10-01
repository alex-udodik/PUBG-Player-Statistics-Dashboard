using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Leaderboard
{
    public class AttributesPlayer
    {
        public string name { get; set; }
        public int rank { get; set; }
        public Stats stats { get; set; }
    }
}
