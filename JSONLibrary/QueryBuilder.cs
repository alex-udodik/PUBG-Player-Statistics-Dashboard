using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class QueryBuilder
    {
        private readonly string BASE_URL = "https://api.pubg.com/shards/steam/";

        //https://api.pubg.com/shards/pc-na/leaderboards/seasonid/squad-fpp
        private readonly string BASE_LEADEROARD_URL = "https://api.pubg.com/shards/";


        public string GetAccountIDQuery(string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players?filter[playerNames]=" + name);

            return builder.ToString();
        }

        public string GetSeasonsListQuery()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "seasons");

            return builder.ToString();
        }

        public string GetSeasonForPlayerQuery(string account_id, string season_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players/" + account_id + "/seasons/" + season_id + "?filter[gamepad]=false");

            return builder.ToString();
        }

        public string GetRankedSeasonForPlayerQuery(string account_id, string season_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players/" + account_id + "/seasons/" + season_id + "/ranked");
            return builder.ToString();
        }

        public string GetMatchQuery(string match_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "matches/" + match_id);
            return builder.ToString();
        }

        public string GetLeaderboardQuery(string shard, string season_id, string mode)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_LEADEROARD_URL + shard + "/" + "leaderboards/" + season_id + "/" + mode);
            return builder.ToString();
        }
    }
}
