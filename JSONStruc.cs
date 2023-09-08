using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    public class JSONStruc
    {
        public string tag_name { get; set; } // Version
        public string html_url { get; set; } // Where to go to get
        public string name { get; set; } // The visible name

        public string published_at { get; set; } // Published date
        public string body { get; set; } // Description

        public string? error { get; set; } // Deprecated

        public JSONStruc()
        {
            error = null;
        }
    }
}
