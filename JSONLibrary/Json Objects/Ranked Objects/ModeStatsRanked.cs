using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Ranked_Objects
{
    public abstract class ModeStatsRanked
    {
        public CurrentTier currentTier { get; set; }

        public double currentRankPoint { get; set; }
        public BestTier bestTier { get; set; }

        public double bestRankPoint { get; set; }
        public double RoundsPlayed { get; set; }
        public double AvgRank { get; set; }

        [Obsolete("AvgSurvivalTime is deprecated")]
        public double AvgSurvivalTime { get; set; }
        public double Top10Ratio { get; set; }
        public double WinRatio { get; set; }
        public double Assists { get; set; }
        public double Wins { get; set; }
        public double Kda { get; set; }

        [Obsolete("Kdr is deprecated")]

        public double Kdr { get; set; }
        public double Kills { get; set; }
        public double Deaths { get; set; }

        [Obsolete("RoundMostKills is deprecated")]

        public double RoundMostKills { get; set; }

        [Obsolete("LongestKill is deprecated")]

        public double LongestKill { get; set; }

        [Obsolete("HeadShotKills is deprecated")]

        public double HeadShotKills { get; set; }

        [Obsolete("HeadShotKillRatio is deprecated")]

        public double HeadShotKillRatio { get; set; }
        public double DamageDealt { get; set; }
        public double Dbnos { get; set; }

        [Obsolete("ReviveRatio is deprecated")]

        public double ReviveRatio { get; set; }

        [Obsolete("Revives is deprecated")]

        public double Revives { get; set; }

        [Obsolete("Heals is deprecated")]

        public double Heals { get; set; }

        [Obsolete("Boosts is deprecated")]

        public double Boosts { get; set; }

        [Obsolete("WeaponsAcquired is deprecated")]

        public double WeaponsAcquired { get; set; }

        [Obsolete("TeamKills is deprecated")]

        public double TeamKills { get; set; }

        [Obsolete("PlayTime is deprecated")]

        public double PlayTime { get; set; }

        [Obsolete("KillStreak is deprecated")]

        public double KillStreak { get; set; } 
    }
}
