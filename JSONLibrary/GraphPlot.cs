using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class GraphPlot
    {
        public double TotalDamageDealt { get; set; }
        public int RoundsPlayed { get; set; }

        public double Adr { get; set; }

        public string playerName { get; set; }
        public DateTime date { get; set; }

        public GraphPlot (double damage, string playerName, DateTime date)
        {
            TotalDamageDealt = damage;
            RoundsPlayed = 1;
            Adr = damage / RoundsPlayed;

            this.playerName = playerName;
            this.date = date;
        }

        public void Add(double damage)
        {
            TotalDamageDealt += damage;
            RoundsPlayed += 1;

            Adr = TotalDamageDealt / RoundsPlayed;
        }

    }
}
