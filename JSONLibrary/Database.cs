using JSONLibrary.Json_Objects.Match;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class Database
    {
        private const string CONNECTION_STRING = "Host = localhost; Username = postgres; Database = PUBG; password = password";

        public Database()
        {

        }

        private async Task CreateInsertQuery(string insertSql, Dictionary<object, object> dict)
        {


            /*
            string matchid = match.data.id;
            string type = match.data.type;
            DateTime date = DateTime.Parse(match.data.attributes.createdAt);
            int duration = match.data.attributes.duration;
            bool isCustomMatch = match.data.attributes.isCustomMatch;
            string matchType = match.data.attributes.matchType;
            string gameMode = match.data.attributes.gameMode;
            string titleid = match.data.attributes.titleId;
            string shardid = match.data.attributes.shardId;
            string mapname = match.data.attributes.mapName;

            string sql = "insert into Match_ (matchid_,type_,createdAt_,duration_,iscustommatch_,matchtype_,gamemode_,titleid_,shardid_,mapname_) " +
                "values(:matchid_,:type_,:createdAt_,:duration_,:iscustommatch_,:matchtype_,:gamemode_,:titleid_,:shardid_,:mapname_)";


            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    await Task.Run(() => conn.Open());

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":matchid_", NpgsqlDbType.Varchar).Value = matchid;
                        cmd.Parameters.Add(":type_", NpgsqlDbType.Varchar).Value = type;
                        cmd.Parameters.Add(":createdAt_", NpgsqlDbType.Timestamp).Value = date;
                        cmd.Parameters.Add(":duration_", NpgsqlDbType.Integer).Value = duration;
                        cmd.Parameters.Add(":iscustommatch_", NpgsqlDbType.Boolean).Value = isCustomMatch;
                        cmd.Parameters.Add(":matchtype_", NpgsqlDbType.Varchar).Value = matchType;
                        cmd.Parameters.Add(":gamemode_", NpgsqlDbType.Varchar).Value = gameMode;
                        cmd.Parameters.Add(":titleid_", NpgsqlDbType.Varchar).Value = titleid;
                        cmd.Parameters.Add(":shardid_", NpgsqlDbType.Varchar).Value = shardid;
                        cmd.Parameters.Add(":mapname_", NpgsqlDbType.Varchar).Value = mapname;

                        cmd.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    Console.WriteLine("ERROR:" + ex.Message);
                }
            }*/
        }
    }
}
