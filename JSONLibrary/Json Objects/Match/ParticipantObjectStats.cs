using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class ParticipantObjectStats
    {
        public int DBNOs { get; set; }
        public int assists { get; set; }
        public int boosts { get; set; }
        public double damageDealt { get; set; }
        public string deathType { get; set; }
        public int headshotKills { get; set; }
        public int heals { get; set; }
        public int killPlace { get; set; }
        public int killStreaks { get; set; }
        public int kills { get; set; }
        public double longestKill { get; set; }
        public string name { get; set; }
        public string playerId { get; set; }
        public int revives { get; set; }
        public double rideDistance { get; set; }
        public int roadKills { get; set; }
        public double swimDistance { get; set; }
        public int teamKills { get; set; }
        public double timeSurvived { get; set; }
        public int vehicleDestroys { get; set; }
        public double walkDistance { get; set; }
        public int weaponsAcquired { get; set; }
        public int winPlace { get; set; }
    }
}
