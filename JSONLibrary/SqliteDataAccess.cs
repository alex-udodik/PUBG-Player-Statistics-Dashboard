using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class SqliteDataAccess
    {
        public static async Task<List<Person>> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = await Task.Run(() => cnn.Query<Person>("select * from Person", new DynamicParameters()));
                return output.ToList();
            }
        }

        public static async Task SavePlayer(Person person)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    await Task.Run(() => cnn.Execute("insert or ignore into Person(name_, accountid_, lowercasename_) values (@name_, @accountid_, @lowercasename_);", person));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public static async Task SavePlayers(List<Person> list)
        {
            List<Task> tasks = new List<Task>();

            foreach (Person person in list)
            {
                tasks.Add(Task.Run(() => SavePlayer(person)));

            }

            await Task.WhenAll(tasks);

        }

        public static async Task<string> CheckName(string name)
        {
            string lowercase = name.ToLower();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = await Task.Run(() => cnn.Query<Person>("select * from Person where lowercasename_ = '" + lowercase + "';", new DynamicParameters()));
                List<Person> list = output.ToList();
                
                foreach(Person person in list)
                {
                    if (person.lowercasename_ == lowercase)
                    {
                        return person.accountid_;
                    }
                }
            }

            return string.Empty;
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
