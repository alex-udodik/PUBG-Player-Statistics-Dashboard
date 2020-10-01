using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Leaderboard
{
    public class Stats
    {
        public int rankPoints { get; set; }
        public int wins { get; set; }
        public int games { get; set; }
        public double winRatio { get; set; }
        public double averageDamage { get; set; }
        public int kills { get; set; }
        public double killDeathRatio { get; set; }
        public double kda { get; set; }
        public double averageRank { get; set; }
        public string tier { get; set; }
        public string subTier { get; set; }
    }
}
