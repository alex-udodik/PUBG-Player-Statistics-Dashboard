using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public abstract class ModeStats
    {
      
        public double Assists { get; set; }
        public double Boosts { get; set; }
        public double DBNOS { get; set; }
        public double DailyKills { get; set; }
        public double DailyWins { get; set; }
        public double DamageDealt { get; set; }
        public double Days { get; set; }
        public double HeadshotKills { get; set; }
        public double Heals { get; set; }

        [Obsolete("killPoints is deprecated")]

        public double KillPoints { get; set; }
        public double Kills { get; set; }
        public double LongestKill { get; set; }
        public double LongestTimeSurvived { get; set; }
        public double Losses { get; set; }
        public double MaxKillStreaks { get; set; }
        public double MostSurvivalTime { get; set; }

        [Obsolete("rankPoints is deprecated")]

        public double RankPoints { get; set; }

        [Obsolete("rankPointsTitle is deprecated")]

        public string RankPointsTitle { get; set; }
        public double Revives { get; set; }
        public double RideDistance { get; set; }
        public double RoadKills { get; set; }
        public double RoundMostKills { get; set; }
        public double RoundsPlayed { get; set; }
        public double Suicides { get; set; }
        public double SwimDistance { get; set; }
        public double TeamKillsteamKills { get; set; }
        public double TimeSurvived { get; set; }
        public double Top10s { get; set; }
        public double VehicleDestroys { get; set; }
        public double WalkDistance { get; set; }
        public double WeaponsAcquired { get; set; }
        public double WeeklyKills { get; set; }
        public double WeeklyWins { get; set; }

        [Obsolete("WinPoints is deprecated")]

        public double WinPoints { get; set; }
        public double Wins { get; set; }

    }
}
