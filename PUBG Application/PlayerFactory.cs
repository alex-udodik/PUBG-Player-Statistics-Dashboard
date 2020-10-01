using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using PUBG_Application.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PUBG_Application
{
    public static class PlayerFactory
    {

        public static async Task<PanelPlayer> BuildPlayer(PanelPlayer player)
        {

            if (player != null && player.NormalStatsObj != null)
            {

                player.CalculatedSoloStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.soloStats));
                player.CalculatedDuoStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.duoStats));
                player.CalculatedSquadStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.squadStats));
                player.CalculatedSoloFppStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.soloFppStats));
                player.CalculatedDuoFppStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.duoFppStats));
                player.CalculatedSquadFppStats = await Task.Run(() => ComputeStats(player.NormalStatsObj.data.attributes.gameModeStats.squadFPPStats));


                player.Matches = await Task.Run(() => GetMatchesParsed(player.AccountObj.data[0].relationships.matches.data));

                player.Matches20Solo = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.Solo, 20, "official"));
                player.Matches20Duo = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.Duo, 20, "official"));
                player.Matches20Squad = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.Squad, 20, "official"));
                player.Matches20SoloFpp = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.SoloFPP, 20, "official"));
                player.Matches20DuoFpp = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.DuoFPP, 20, "official"));
                player.Matches20SquadFpp = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.SquadFPP, 20, "official"));

                player.Matches20RankedTpp = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.RankedTPP, 20, "competitive"));
                player.Matches20RankedFpp = await Task.Run(() => FilterMatches(player.Matches, Values.StatType.RankedFPP, 20, "competitive"));

                

            }

            if (player != null && player.RankedStatsObj != null)
            {
                if (player.RankedStatsObj.data.attributes.rankedGameModeStats.squad != null)
                {
                    player.CalculatedRankedTppStats = await Task.Run(() =>
                    ComputeStatsRanked(player.RankedStatsObj.data.attributes.rankedGameModeStats.squad, player.Matches, player.Name));
                }
                else
                {
                    player.CalculatedRankedTppStats = await Task.Run(() => ComputeStatsRanked(null, null, null));
                }
                if (player.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp != null)
                {
                    player.CalculatedRankedFppStats = await Task.Run(() =>
                    ComputeStatsRanked(player.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp, player.Matches, player.Name));
                }
                else
                {
                    player.CalculatedRankedFppStats = await Task.Run(() => ComputeStatsRanked(null, null, null)) ;
                }
            }


            return player;
        }

        private static async Task<List<RootMatch>> FilterMatches(List<RootMatch> matches, Values.StatType type, int maxSize, string matchtypeTarget)
        {

            List<RootMatch> list = new List<RootMatch>();

            foreach (RootMatch match in matches)
            {
                string gameMode = match.data.attributes.gameMode;
                string matchType = match.data.attributes.matchType;

                if (gameMode == Values.GetEnumString(type).ToLower() &&
                    matchType == matchtypeTarget &&
                    list.Count < maxSize)
                {
                    list.Add(match);
                }

                if (list.Count == maxSize)
                {
                    break;
                }
            }

            return list;
        }

       
        private static async Task<List<RootMatch>> GetMatchesParsed(List<JSONLibrary.Json_Objects.AccountID.DataMatches> matches)
        {
            List<Task<RootMatch>> tasks = new List<Task<RootMatch>>();

            foreach (DataMatches match in matches)
            {
                tasks.Add(Task.Run(() => ParseMatch(match.id)));
            }

            var results = await Task.WhenAll(tasks);

            List<RootMatch> list = results.ToList();

            return list;
        }

        private static async Task<RootMatch> ParseMatch(string match_id)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor = new QueryExecutor(builder.GetMatchQuery(match_id));
            Tuple<string, int> tuple = await executor.ExecuteQueryAsync();

            RootMatch match = JsonParser.ParseMatchData(tuple).Item1;

            return match;
        }

        private static async Task<UnRankedObject> ComputeStats(ModeStats stats)
        {
            if (stats != null)
            {
                return new UnRankedObject()
                {
                    GamesPlayed = (int)stats.RoundsPlayed,
                    Wins = (int)stats.Wins,
                    WinPercent = Math.Round(StatsCalculation.GetWinRatio(stats.Wins, stats.RoundsPlayed), 2),
                    AvgSurvivalTime = Math.Round(StatsCalculation.GetAverageSurvivedTime(stats.TimeSurvived, stats.RoundsPlayed), 2),
                    Adr = (int)StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed),
                    HeadshotRatio = Math.Round(StatsCalculation.GetHeadshotRatio(stats.HeadshotKills, stats.Kills), 2),
                    MaxKills = (int)stats.RoundMostKills,
                    LongestKill = Math.Round(stats.LongestKill, 2),
                    DbnosPerRound = Math.Round(StatsCalculation.GetKnocksPerRound(stats.DBNOS, stats.RoundsPlayed), 2),

                    FraggerRating = Math.Round(StatsCalculation.GetFraggerRating(
                        StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed),
                        StatsCalculation.GetHeadshotRatioBelowOne(stats.HeadshotKills, stats.Kills),
                        StatsCalculation.GetAverageSurvivedTimeBase10(stats.TimeSurvived, stats.RoundsPlayed),
                        StatsCalculation.GetWinRatioBelowOne(stats.Wins, stats.RoundsPlayed)), 0)
                };
            }
            else
            {
                return new UnRankedObject()
                {
                    GamesPlayed = 0,
                    Wins = 0,
                    WinPercent = 0,
                    AvgSurvivalTime = 0,
                    Adr = 0,
                    HeadshotRatio = 0,
                    MaxKills = 0,
                    LongestKill = 0,
                    DbnosPerRound = 0,
                    FraggerRating = 0

                };
            }
        }

        private static async Task<RankedObject> ComputeStatsRanked(ModeStatsRanked stats, List<RootMatch> matches, string playername)
        {
            if (stats != null)
            {
                RankedObject ranked = GetRankedObject((int)stats.currentRankPoint);


                ranked.GamesPlayed = (int)stats.RoundsPlayed;
                ranked.Wins = (int)stats.Wins;
                ranked.WinPercent = Math.Round(StatsCalculation.GetWinRatio(stats.Wins, stats.RoundsPlayed), 2);
                ranked.AverageRank = Math.Round(stats.AvgRank, 2);
                ranked.TopTenPercent = Math.Round(stats.Top10Ratio * 100, 2);
                ranked.Adr = (int)Math.Round(StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed), 0);
                ranked.Kd = Math.Round(StatsCalculation.GetKD(stats.Kills, stats.Deaths), 2);
                ranked.Kda = Math.Round(StatsCalculation.GetKDA(stats.Kills, stats.Assists, stats.Deaths), 2);
                ranked.DbnosPerRound = Math.Round(StatsCalculation.GetKnocksPerRound(stats.Dbnos, stats.RoundsPlayed), 2);
                ranked.FraggerRating = await Task.Run(() => ComputeRankedFraggerRating(matches, playername));

                return ranked;
            }
            else
            {
                RankedObject ranked = GetRankedObject(0);


                ranked.GamesPlayed = 0;
                ranked.Wins = 0;
                ranked.WinPercent = 0;
                ranked.AverageRank = 0;
                ranked.TopTenPercent = 0;
                ranked.Adr = 0;
                ranked.Kd = 0;
                ranked.Kda = 0;
                ranked.DbnosPerRound = 0;
                ranked.FraggerRating = 0;
                return ranked;
            }
        }

        private static async Task<double> ComputeRankedFraggerRating(List<RootMatch> matches, string playername)
        {
            

            if (matches.Count == 0)
            {
                return 0;
            }
            else
            {
                double totalKills = 0;
                double headshotKills = 0;
                double damageDealt = 0;
                double timeSurvived = 0;
                double winPlace = 0;

                int count = 0;

                foreach (RootMatch match in matches)
                {
                    if (count == 20)
                    {
                        break;
                    }

                    if (match.data.attributes.matchType == "competitive")
                    {
                        foreach (MatchSingleObject participant in match.included)
                        {

                            if (participant.type == "participant")
                            {
                                ParticipantObject matchplayer = (ParticipantObject)participant;
                                string name = matchplayer.attributes.stats.name;

                                if (name == playername)
                                {
                                    totalKills += matchplayer.attributes.stats.kills;
                                    headshotKills += matchplayer.attributes.stats.headshotKills;
                                    damageDealt += matchplayer.attributes.stats.damageDealt;
                                    timeSurvived += matchplayer.attributes.stats.timeSurvived;

                                    if (matchplayer.attributes.stats.winPlace == 1)
                                    {
                                        winPlace += matchplayer.attributes.stats.winPlace;
                                    }
                                    

                                    count++;
                                    break;
                                }


                            }
                        }
                    }
                    
                }

                double adr = StatsCalculation.GetAdr(damageDealt, count);
                double headshotratio = StatsCalculation.GetHeadshotRatioBelowOne(headshotKills, totalKills);
                double survivedtime = StatsCalculation.GetAverageSurvivedTimeBase10(timeSurvived, count);
                double winratepercent = StatsCalculation.GetWinRatioBelowOne(winPlace, count);

                double fraggerRating = Math.Round(StatsCalculation.GetFraggerRating(adr, headshotratio, survivedtime, winratepercent), 0);

                return fraggerRating;
            }
        }


        private static RankedObject GetRankedObject(int rankPoints)
        {


            // bronze
            if (rankPoints > 0 && rankPoints < 1500)
            {
                return new RankedObject(Resources.Bronze_5, Values.RankTitle.Silver, Values.RankLevel.V);
            }

            // silver
            else if (rankPoints >= 1500 && rankPoints < 2000)
            {
                if (rankPoints >= 1500 && rankPoints < 1600)
                {

                    return new RankedObject(Resources.Silver_5, Values.RankTitle.Silver, Values.RankLevel.V);
                }

                else if (rankPoints >= 1600 && rankPoints < 1700)
                {
                    return new RankedObject(Resources.Silver_4, Values.RankTitle.Silver, Values.RankLevel.IV);
                }

                else if (rankPoints >= 1700 && rankPoints < 1800)
                {
                    return new RankedObject(Resources.Silver_3, Values.RankTitle.Silver, Values.RankLevel.III);
                }

                else if (rankPoints >= 1800 && rankPoints < 1900)
                {
                    return new RankedObject(Resources.Silver_2, Values.RankTitle.Silver, Values.RankLevel.II);
                }

                else if (rankPoints >= 1900 && rankPoints < 2000)
                {
                    return new RankedObject(Resources.Silver_1, Values.RankTitle.Silver, Values.RankLevel.I);
                }
            }

            // gold
            else if (rankPoints >= 2000 && rankPoints < 2500)
            {
                if (rankPoints >= 2000 && rankPoints < 2100)
                {
                    return new RankedObject(Resources.Gold_5, Values.RankTitle.Gold, Values.RankLevel.V);
                }

                else if (rankPoints >= 2100 && rankPoints < 2200)
                {
                    return new RankedObject(Resources.Gold_4, Values.RankTitle.Gold, Values.RankLevel.IV);
                }

                else if (rankPoints >= 2200 && rankPoints < 2300)
                {
                    return new RankedObject(Resources.Gold_3, Values.RankTitle.Gold, Values.RankLevel.III);
                }

                else if (rankPoints >= 2300 && rankPoints < 2400)
                {
                    return new RankedObject(Resources.Gold_2, Values.RankTitle.Gold, Values.RankLevel.II);
                }

                else if (rankPoints >= 2400 && rankPoints < 2500)
                {
                    return new RankedObject(Resources.Gold_1, Values.RankTitle.Gold, Values.RankLevel.I);
                }
            }

            // plat
            else if (rankPoints >= 2500 && rankPoints < 3000)
            {
                if (rankPoints >= 2500 && rankPoints < 2600)
                {
                    return new RankedObject(Resources.Platinum_5, Values.RankTitle.Platinum, Values.RankLevel.V);
                }

                else if (rankPoints >= 2600 && rankPoints < 2700)
                {
                    return new RankedObject(Resources.Platinum_4, Values.RankTitle.Platinum, Values.RankLevel.IV);
                }

                else if (rankPoints >= 2700 && rankPoints < 2800)
                {
                    return new RankedObject(Resources.Platinum_3, Values.RankTitle.Platinum, Values.RankLevel.III);
                }

                else if (rankPoints >= 2800 && rankPoints < 2900)
                {
                    return new RankedObject(Resources.Platinum_2, Values.RankTitle.Platinum, Values.RankLevel.II);
                }

                else if (rankPoints >= 2900 && rankPoints < 3000)
                {
                    return new RankedObject(Resources.Platinum_1, Values.RankTitle.Platinum, Values.RankLevel.I);
                }
            }

            // diamond
            else if (rankPoints >= 3000 && rankPoints < 3500)
            {
                if (rankPoints >= 3000 && rankPoints < 3100)
                {
                    return new RankedObject(Resources.Diamond_5, Values.RankTitle.Diamond, Values.RankLevel.V);

                }

                else if (rankPoints >= 3100 && rankPoints < 3200)
                {
                    return new RankedObject(Resources.Diamond_4, Values.RankTitle.Diamond, Values.RankLevel.IV);

                }

                else if (rankPoints >= 3200 && rankPoints < 3300)
                {
                    return new RankedObject(Resources.Diamond_3, Values.RankTitle.Diamond, Values.RankLevel.III);

                }

                else if (rankPoints >= 3300 && rankPoints < 3400)
                {
                    return new RankedObject(Resources.Diamond_2, Values.RankTitle.Diamond, Values.RankLevel.II);

                }

                else if (rankPoints >= 3400 && rankPoints < 3500)
                {
                    return new RankedObject(Resources.Diamond_1, Values.RankTitle.Diamond, Values.RankLevel.I);

                }
            }

            // master
            else if (rankPoints >= 3500)
            {
                return new RankedObject(Resources.Master, Values.RankTitle.Master, Values.RankLevel.Master);

            }

            // unranked
            else
            {
                return new RankedObject(Resources.Unranked, Values.RankTitle.Unranked, Values.RankLevel.Unranked);

            }

            return null;
        }

        public static Image GetRankedImage(int rankPoints)
        {


            // bronze
            if (rankPoints > 0 && rankPoints < 1500)
            {
                return Resources.Bronze_5;
            }

            // silver
            else if (rankPoints >= 1500 && rankPoints < 2000)
            {
                if (rankPoints >= 1500 && rankPoints < 1600)
                {

                    return Resources.Silver_5;
                }

                else if (rankPoints >= 1600 && rankPoints < 1700)
                {
                    return Resources.Silver_4;
                }

                else if (rankPoints >= 1700 && rankPoints < 1800)
                {
                    return Resources.Silver_3;
                }

                else if (rankPoints >= 1800 && rankPoints < 1900)
                {
                    return Resources.Silver_2;
                }

                else if (rankPoints >= 1900 && rankPoints < 2000)
                {
                    return Resources.Silver_1;
                }
            }

            // gold
            else if (rankPoints >= 2000 && rankPoints < 2500)
            {
                if (rankPoints >= 2000 && rankPoints < 2100)
                {
                    return Resources.Gold_5;
                }

                else if (rankPoints >= 2100 && rankPoints < 2200)
                {
                    return Resources.Gold_4;
                }

                else if (rankPoints >= 2200 && rankPoints < 2300)
                {
                    return Resources.Gold_3;
                }

                else if (rankPoints >= 2300 && rankPoints < 2400)
                {
                    return Resources.Gold_2;
                }

                else if (rankPoints >= 2400 && rankPoints < 2500)
                {
                    return Resources.Gold_1;
                }
            }

            // plat
            else if (rankPoints >= 2500 && rankPoints < 3000)
            {
                if (rankPoints >= 2500 && rankPoints < 2600)
                {
                    return Resources.Platinum_5;
                }

                else if (rankPoints >= 2600 && rankPoints < 2700)
                {
                    return Resources.Platinum_4;
                }

                else if (rankPoints >= 2700 && rankPoints < 2800)
                {
                    return Resources.Platinum_3;
                }

                else if (rankPoints >= 2800 && rankPoints < 2900)
                {
                    return Resources.Platinum_2;
                }

                else if (rankPoints >= 2900 && rankPoints < 3000)
                {
                    return Resources.Platinum_1;
                }
            }

            // diamond
            else if (rankPoints >= 3000 && rankPoints < 3500)
            {
                if (rankPoints >= 3000 && rankPoints < 3100)
                {
                    return Resources.Diamond_5;

                }

                else if (rankPoints >= 3100 && rankPoints < 3200)
                {
                    return Resources.Diamond_4;

                }

                else if (rankPoints >= 3200 && rankPoints < 3300)
                {
                    return Resources.Diamond_3;

                }

                else if (rankPoints >= 3300 && rankPoints < 3400)
                {
                    return Resources.Diamond_2;

                }

                else if (rankPoints >= 3400 && rankPoints < 3500)
                {
                    return Resources.Diamond_1;

                }
            }

            // master
            else if (rankPoints >= 3500)
            {
                return Resources.Master;

            }

            // unranked
            else
            {
                return Resources.Unranked;

            }

            return null;
        }

    }
}
