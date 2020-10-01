using JSONLibrary.Json_Objects.Ranked_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONLibrary.Json_Objects.Ranked_Objects
{
    public class Data
    {
        public string Type { get; set; }
        public Attributes attributes { get; set; }

        public Relationships relationships { get; set; }
        

    }
}
