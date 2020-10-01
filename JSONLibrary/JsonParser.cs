using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Leaderboard;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public static class JsonParser
    {
        
        public static Tuple<RootAccountIDObject, int> ParseAccountID(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }
            else if (pair.Item1 == null)
            {
                Tuple<RootAccountIDObject, int> error = Tuple.Create<RootAccountIDObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootAccountIDObject obj = JsonConvert.DeserializeObject<RootAccountIDObject>(pair.Item1);
                Tuple<RootAccountIDObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }

        public static Tuple<RootRankedStatsObject, int> ParseRankedSeasonStats(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootRankedStatsObject, int> error = Tuple.Create<RootRankedStatsObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootRankedStatsObject obj = JsonConvert.DeserializeObject<RootRankedStatsObject>(pair.Item1);
                Tuple<RootRankedStatsObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }
        public static Tuple<RootNormalStatsObject, int> ParseNormalSeasonStats(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootNormalStatsObject, int> error = Tuple.Create<RootNormalStatsObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootNormalStatsObject obj = JsonConvert.DeserializeObject<RootNormalStatsObject>(pair.Item1);
                Tuple<RootNormalStatsObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }

        public static Tuple<RootMatch, int> ParseMatchData(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootMatch, int> error = Tuple.Create<RootMatch, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootMatch obj = JsonConvert.DeserializeObject<RootMatch>(pair.Item1);
                Tuple<RootMatch, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }

        public static Tuple<RootLeaderboard, int> ParseLeaderboard(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootLeaderboard, int> error = Tuple.Create<RootLeaderboard, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootLeaderboard obj = JsonConvert.DeserializeObject<RootLeaderboard>(pair.Item1);
                Tuple<RootLeaderboard, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }
    }
}
