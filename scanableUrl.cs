using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    /// <summary>
    /// Custom datatype to decide what url to check. Allows multiple website to be checked
    /// </summary>
    public class scanableUrl
    {
        // Required things that make uri
        public string ?OWNERNAME { get; set; }
        public string ?REPONAME { get; set; }

        public string ?currentVersion { get; set; }

        // Actions
        public bool forceUpdate { get; set; }

        // Other settings
        public bool isConsoleApp { get; set; }

        public scanableUrl() {
            forceUpdate = false;
            isConsoleApp = true;
        }
    }
}
