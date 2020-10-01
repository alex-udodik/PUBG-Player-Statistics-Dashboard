using JSONLibrary.Json_Objects.Match;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class DoublePlayerMatchFiltering : MatchFiltering
    {
        private List<RootMatch> matchesA;
        private string playerNameA;

        private List<RootMatch> matchesB;
        private string playerNameB;

        private Values.StatType type;
        private string matchType;

        public DoublePlayerMatchFiltering(List<RootMatch> matchesA, string playerNameA, 
            List<RootMatch> matchesB, string playerNameB, 
            Values.StatType type, string matchType)
        {
            this.matchesA = matchesA;
            this.playerNameA = playerNameA;

            this.matchesB = matchesB;
            this.playerNameB = playerNameB;

            this.type = type;
            this.matchType = matchType;
        }

        public Tuple<List<GraphPlot>, List<GraphPlot>> GetList()
        {
            Dictionary<string, GraphPlot> dictA = this.GetDictionary(this.matchesA, this.type, this.matchType, this.playerNameA);
            Dictionary<string, GraphPlot> dictB = this.GetDictionary(this.matchesB, this.type, this.matchType, this.playerNameB);

            List<GraphPlot> plotA = new List<GraphPlot>();
            List<GraphPlot> plotB = new List<GraphPlot>();


            DateTime timeOldA = DateTime.Now;
            DateTime timeRecentA = DateTime.Now;

            DateTime timeOldB = DateTime.Now;
            DateTime timeRecentB = DateTime.Now;


            if (dictA.Count != 0 && dictB.Count != 0)
            {
                timeOldA = this.GetOldestDate(dictA);
                timeRecentA = this.GetRecentDate(dictA);

                timeOldB = this.GetOldestDate(dictB);
                timeRecentB = this.GetRecentDate(dictB);

                int oldest = DateTime.Compare(timeOldA, timeOldB);

                DateTime oldestDate = DateTime.Now;

                if (oldest < 0)
                {
                    oldestDate = timeOldA;
                }
                else
                {
                    oldestDate = timeOldB;
                }

                int recent = DateTime.Compare(timeRecentA, timeRecentB);

                DateTime mostRecent = DateTime.Now;

                if (recent > 0)
                {
                    mostRecent = timeRecentA;
                }
                else
                {
                    mostRecent = timeRecentB;
                }

                for (DateTime i = oldestDate; i <= mostRecent; i = i.AddDays(1))
                {
                    int oldYear = i.Year;
                    int oldMonth = i.Month;
                    int oldDay = i.Day;

                    string tempDate = oldYear.ToString() + "-" + oldMonth.ToString() + "-" + oldDay.ToString();

                    if (dictA.ContainsKey(tempDate) && dictB.ContainsKey(tempDate))
                    {
                        plotA.Add(dictA[tempDate]);
                        plotB.Add(dictB[tempDate]);
                    }
                    else if (dictA.ContainsKey(tempDate) && !dictB.ContainsKey(tempDate))
                    {
                        plotA.Add(dictA[tempDate]);
                        plotB.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    }
                    else if (!dictA.ContainsKey(tempDate) && dictB.ContainsKey(tempDate))
                    {
                        plotA.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                        plotB.Add(dictB[tempDate]);
                    }
                    else
                    {
                        plotA.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                        plotB.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    }


                }

                return Tuple.Create(plotA, plotB);
            }
            else if (dictA.Count != 0 && dictB.Count == 0)
            {
                timeOldA = this.GetOldestDate(dictA);
                timeRecentA = this.GetRecentDate(dictA);

                for (DateTime i = timeOldA; i <= timeRecentA; i = i.AddDays(1))
                {
                    int oldYear = i.Year;
                    int oldMonth = i.Month;
                    int oldDay = i.Day;

                    string tempDate = oldYear.ToString() + "-" + oldMonth.ToString() + "-" + oldDay.ToString();

                    if (dictA.ContainsKey(tempDate))
                    {
                        plotA.Add(dictA[tempDate]);
                        plotB.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    }
                }

                return Tuple.Create(plotA, plotB);

            }
            else if (dictB.Count != 0 && dictA.Count == 0)
            {
                timeOldB = this.GetOldestDate(dictB);
                timeRecentB = this.GetRecentDate(dictB);

                for (DateTime i = timeOldB; i <= timeRecentB; i = i.AddDays(1))
                {
                    int oldYear = i.Year;
                    int oldMonth = i.Month;
                    int oldDay = i.Day;

                    string tempDate = oldYear.ToString() + "-" + oldMonth.ToString() + "-" + oldDay.ToString();

                    if (dictA.ContainsKey(tempDate))
                    {
                        
                        plotB.Add(dictA[tempDate]);
                        plotA.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    }
                }

                return Tuple.Create(plotA, plotB);
            }
            else
            {
                DateTime timeOld = DateTime.Now.AddDays(-1);
                DateTime timeRecent = DateTime.Now;

                for (DateTime i = timeOld; i <= timeRecent; i = i.AddDays(1))
                {
                    int oldYear = i.Year;
                    int oldMonth = i.Month;
                    int oldDay = i.Day;

                    string tempDate = oldYear.ToString() + "-" + oldMonth.ToString() + "-" + oldDay.ToString();

                    
                    plotB.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    plotA.Add(new GraphPlot(0, "", DateTime.Parse(tempDate)));
                    
                }
                return Tuple.Create(plotA, plotB);

            }
        }

        public DateTime GetOldestDate(Dictionary<string, GraphPlot> dict)
        {
            DateTime oldest = DateTime.Parse(dict.Keys.First());

            foreach(KeyValuePair<string, GraphPlot> pair in dict)
            {
                DateTime currentKey = DateTime.Parse(pair.Key);

                int result = DateTime.Compare(currentKey, oldest);

                if (result < 0)
                {
                    oldest = currentKey;
                }
            }

            return oldest;
        }

        private DateTime GetRecentDate(Dictionary<string, GraphPlot> dict)
        {
            DateTime recent = DateTime.Parse(dict.Keys.First());

            foreach (KeyValuePair<string, GraphPlot> pair in dict)
            {
                DateTime currentKey = DateTime.Parse(pair.Key);

                int result = DateTime.Compare(currentKey, recent);

                if (result > 0)
                {
                    recent = currentKey;
                }
            }

            return recent;
        }
        private Dictionary<string, GraphPlot> GetDictionary(List<RootMatch> matches, Values.StatType type, string matchType, string playerName)
        {
            Dictionary<string, GraphPlot> dict = new Dictionary<string, GraphPlot>();

            foreach (RootMatch match in matches)
            {
                string currentMatchGameMode = match.data.attributes.gameMode;
                string currentMatchType = match.data.attributes.matchType;

                if (currentMatchGameMode == Values.GetEnumString(type).ToLower() && currentMatchType == matchType)
                {
                    foreach (MatchSingleObject participant in match.included)
                    {
                        if (participant.type == "participant")
                        {
                            ParticipantObject matchPlayer = (ParticipantObject)participant;
                            string matchPlayerName = matchPlayer.attributes.stats.name;

                            if (matchPlayerName == playerName)
                            {
                                string matchDate = match.data.attributes.createdAt;

                                DateTime matchTime = DateTime.Parse(matchDate);

                                string date = matchTime.Year.ToString() + "-" + matchTime.Month.ToString() + "-" + matchTime.Day.ToString();

                                double damageDealtInMatch = matchPlayer.attributes.stats.damageDealt;

                                if (!dict.ContainsKey(date))
                                {
                                    dict.Add(date, new GraphPlot(damageDealtInMatch, playerName, matchTime));
                                }
                                else
                                {
                                    dict[date].Add(damageDealtInMatch);
                                }

                                break;
                            }
                        }

                    }
                }
            }

            return dict;
        }
    }
}
