using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace JSONLibrary
{
    public class QueryExecutor
    {
        private readonly string API_KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9" +
            ".eyJqdGkiOiJlZGYwZTE4MC03YWI3LTAxMzgtZDM1YS0wMDFlODhkODU2NjAiLCJpc" +
            "3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTg5NzUyOTA4LCJwdWIiOiJibHVlaG9sZSI" +
            "sInRpdGxlIjoicHViZyIsImFwcCI6ImFsZXgtbXlzdGVyeW1hIn0.eImkcaol-TKjn" +
            "nNMq1JXaNgFOTtXYMVbLaE_97q3FaI";

        private string url;
        public QueryExecutor(string url)
        {
            this.url = url;
        }

        public async Task<Tuple<string, int>> ExecuteQueryAsync()
        {
            if (url == null)
            {
                return null;
            }

            Tuple<string, int> pair;
            if (url != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);
                request.Method = "GET";
                request.Headers["Authorization"] = "Bearer " + this.API_KEY;
                request.Accept = "application/vnd.api+json";
                StreamReader reader;
                HttpWebResponse response = null;
                HttpStatusCode statusCode;


                try
                {
                    var res = await Task.Run(() => request.GetResponseAsync());
                    response = (HttpWebResponse)res;
                }
                catch (WebException webException)
                {
                    response = (HttpWebResponse)webException.Response;
                }

                statusCode = response.StatusCode;

                Stream receiveStream = await Task.Run(() => response.GetResponseStream()); 
                reader = new StreamReader(receiveStream, Encoding.GetEncoding("utf-8"));

                if (reader != null)
                {
                    
                    Console.WriteLine("Response Code: " + (int)statusCode + " - " + statusCode.ToString());

                    if ((int)statusCode != 200)
                    {
                        pair = Tuple.Create<string, int>(null, (int)statusCode);
                        return pair;
                    }
                    else
                    {
                        string json = reader.ReadToEnd();
                        pair = Tuple.Create(json, (int)statusCode);
                        return pair;

                    }
                }
            }
            
            return null;
        }
       
    }

    
}
