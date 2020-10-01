using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JSONLibrary
{
    public class SinglePlayerMatchFiltering : MatchFiltering 
    {
        private List<RootMatch> matches;
        private Values.StatType type;
        private string matchType;
        private string playerName;

        public SinglePlayerMatchFiltering(List<RootMatch> matches, Values.StatType type, string matchType, string playerName)
        {
            this.matches = matches;
            this.type = type;
            this.matchType = matchType;
            this.playerName = playerName;
        }

        public List<GraphPlot> GetList()
        {
            //DateTime time = DateTime.Parse("2016-07-24T18:47:36Z").ToUniversalTime();

            Dictionary<string, GraphPlot> dict = new Dictionary<string, GraphPlot>();


            foreach(RootMatch match in this.matches)
            {
                string currentMatchGameMode = match.data.attributes.gameMode;
                string currentMatchType = match.data.attributes.matchType;

                if (currentMatchGameMode == Values.GetEnumString(this.type).ToLower() && currentMatchType == this.matchType)
                {
                    foreach (MatchSingleObject participant in match.included)
                    {
                        if (participant.type == "participant")
                        {
                            ParticipantObject matchPlayer = (ParticipantObject)participant;
                            string matchPlayerName = matchPlayer.attributes.stats.name;

                            if (matchPlayerName == this.playerName)
                            {
                                string matchDate = match.data.attributes.createdAt;

                                DateTime matchTime = DateTime.Parse(matchDate);

                                string date = matchTime.Year.ToString() + matchTime.Month.ToString() + matchTime.Day.ToString();
                                
                                double damageDealtInMatch = matchPlayer.attributes.stats.damageDealt;

                                if (!dict.ContainsKey(date))
                                {
                                    dict.Add(date, new GraphPlot(damageDealtInMatch, this.playerName, matchTime));
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


            List<GraphPlot> graphPlots = new List<GraphPlot>();

            foreach(KeyValuePair<string, GraphPlot> pair in dict)
            {
                graphPlots.Add(pair.Value);
            }

            
            List<GraphPlot> graphPlotsReversed = new List<GraphPlot>();

            for (int i = graphPlots.Count - 1; i >= 0; i--)
            {
                graphPlotsReversed.Add(graphPlots[i]);
            }

            return graphPlotsReversed;
        }

        
    }
}
