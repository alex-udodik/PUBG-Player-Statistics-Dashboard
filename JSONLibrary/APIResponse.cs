using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class APIResponse
    {
        private int code = 200;
        public APIResponse(int code)
        {
            this.code = code;
        }

        public string GetDetailedResponse()
        {
            switch (this.code)
            {
                case 400:
                    return "Bad request";
                case 401:
                    return "API key invalid or missing";
                case 404:
                    return "The specified resource was not found (Name is case sensitive)";
                case 415:
                    return "Content type incorrect or not specified";
                case 429:
                    return "Too many requests";
                default:
                    return string.Empty;
            }
        }

        public string GetFormattedResponseAccounNameLookUp()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Account ID lookup failed");
            builder.Append(Environment.NewLine);
            builder.Append("Error code: " + this.code.ToString());
            builder.Append(Environment.NewLine);
            builder.Append(this.GetDetailedResponse());
            return builder.ToString();
        }

        public string GetFormattedResponseStatLookUp()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Casual stats fetch failed");
            builder.Append(Environment.NewLine);
            builder.Append("Error code: " + this.code.ToString());
            builder.Append(Environment.NewLine);
            builder.Append(this.GetDetailedResponse());
            return builder.ToString();
        }

        public string GetFormattedResponseRankedLookUp()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Ranked stats fetch failed");
            builder.Append(Environment.NewLine);
            builder.Append("Error code: " + this.code.ToString());
            builder.Append(Environment.NewLine);
            builder.Append(this.GetDetailedResponse());
            return builder.ToString();
        }

        public string GetFormattedResponseLeaderboardLookUp()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Leaderboard data fetch failed");
            builder.Append(Environment.NewLine);
            builder.Append("Error code: " + this.code.ToString());
            builder.Append(Environment.NewLine);
            builder.Append(this.GetDetailedResponse());
            return builder.ToString();
        }
    }
}
