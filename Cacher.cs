using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    internal class Cacher
    {
        internal string? JSONCache = "";
        internal DateTime JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5));

        internal void clearCache()
        {
            JSONCache = null;
            JSONCacheAge = DateTime.Now;
        }
    }
}
