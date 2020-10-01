using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Regular_Objects
{
    public class Data
    {
        public string Type { get; set; }
        public Attributes attributes { get; set; }

        public Relationships relationships { get; set; }
    }
}
