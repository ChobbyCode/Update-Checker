using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    /// <summary>
    /// updateObject bob = checker.getStuff...();
    /// </summary>
    public class updateObject
    {
        // Main Information
        public string? version { get; set; }
        public string? versionName { get; set; }
        public string? dateReleased { get; set; }
        public string? webURL { get; set; }

        public bool isUpdate { get; set; }

        // Download related stuff
        public string[] downloadAssets { get; set; }


        // Other Information
        public string? description { get; set; }
    }
}
