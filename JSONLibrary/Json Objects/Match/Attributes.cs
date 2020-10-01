using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Match
{
    public class Attributes
    {
        public int duration { get; set; }
        public object stats { get; set; }
        public string seasonState { get; set; }
        public bool isCustomMatch { get; set; }
        public string matchType { get; set; }
        public string createdAt { get; set; }
        public string gameMode { get; set; }
        public string titleId { get; set; }
        public string shardId { get; set; }
        public object tags { get; set; }
        public string mapName { get; set; }

    }
}
