using JSONLibrary.Json_Objects.Ranked_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.AccountID
{
    public class Data
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }

        public Relationships relationships { get; set; }
        public Links links { get; set; }
    }
}
