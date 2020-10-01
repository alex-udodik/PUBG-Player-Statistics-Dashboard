using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Application
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(params object[] args)
        {
            Args = args;
        }

        public object[] Args { get; set; }
    }
}
