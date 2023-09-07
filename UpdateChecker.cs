using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    public class UpdateChecker
    {
        // These variables can be set with the use of a method
        public string? OWNERNAME = null;
        public string? REPONAME = null;
        public string? VERSION = null;

        //Cache the JSON to minimise the amount of API calls to the github api so that no get sued or something like that :
        internal JSONStruc? JSONCache = null;
        internal DateTime JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5));

        // Constructor File

        /// <summary>
        /// Gets the latest version from tag
        /// </summary>
        /// <returns>Returns the latest version in tag in string</returns>
        public async Task<string> LatestVersionTag()
        {
            if (!validCreds())
            {
                // Error message
                return "Please configure the application before making Http calls. Read the documentation at https://github.com/ChobbyCode/Update-Checker";
            }
            else
            {
                // Create Variables

                JSONStruc? response;

                if (possibleCache())
                {
                    // Recache the information
                    // Create a url to pass thru
                    string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";

                    // Makes and stores http call
                    apiCaller apiCaller = new apiCaller();
                    response = await apiCaller.mkHttpCall(url);

                    // Writes to cache
                    cache(response);
                }
                else
                {
                    // Get information from cache
                    response = JSONCache;
                }

                // Get information
                string versionTag = response!.tag_name;

                //Returns the version
                return versionTag;
            }
        }


        /// <summary>
        /// Returns a part of a version. If version is v6.1.2, and input was 0 it would return 6 if 1 it would return 1 and if 2 it would return 2
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public async Task<string> GetVersionPart(int part)
        {

            if (!validCreds())
            {
                return "Please configure the application before making Http calls. Read the documentation at https://github.com/ChobbyCode/Update-Checkers";
            }
            else
            {
                JSONStruc? response;
                if (possibleCache())
                {
                    // Recache the information
                    // Create a url to pass thru
                    string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";

                    // Makes and stores api call
                    apiCaller apiCaller = new apiCaller();
                    response = await apiCaller.mkHttpCall(url);

                    // Writes to cache
                    cache(response);
                }
                else
                {
                    // Get information from cache
                    response = JSONCache;
                }

                // Get information
                string versionTag = response!.tag_name;

                Parser parser = new Parser();

                //Returns the version
                return parser.parseVersion(versionTag)[part];
            }
        }


        public async Task<bool> CheckForUpdates(string currentVersionTag)
        {
            if (currentVersionTag == null)
            {
                // Makes sure the input isn't null and is valid
                return false;
            }

            string currentVersion = currentVersionTag + "."; // Adds a dot to the end to prevent out of range error

            // Parse current version
            Parser parser = new Parser();
            string[] currentVersionSplit = parser.parseVersion(currentVersion);

            if (!validCreds())
            {
                return false;
            }

            JSONStruc response;

            if (possibleCache())
            {
                // Set the URL
                string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";

                // Make api client and calls
                apiCaller apiCaller = new apiCaller();
                response = await apiCaller.mkHttpCall(url);

                // Writes to cache
                cache(response);
            }
            else
            {
                response = JSONCache;
            }

            // Get information
            string[] latestVersionSplit = parser.parseVersion(response!.tag_name);

            if (Convert.ToInt16(latestVersionSplit[0]) > Convert.ToInt16(currentVersionSplit[0])
                || Convert.ToInt16(latestVersionSplit[1]) > Convert.ToInt16(currentVersionSplit[1])
                || Convert.ToInt16(latestVersionSplit[2]) > Convert.ToInt16(currentVersionSplit[2]))
            {
                return true;
            }

            // Else
            return false;


        }

        internal bool validCreds()
        {
            if (OWNERNAME == null || REPONAME == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        // Caching functions

        internal void clearCache()
        {
            // Clear the cache
            JSONCache = null;
            JSONCacheAge = DateTime.Now;
        }

        /// <summary>
        /// Cacher
        /// </summary>
        /// <returns>True if can recache and false if can't recache</returns>
        internal bool possibleCache()
        {
            if (JSONCacheAge.TimeOfDay < DateTime.Now.TimeOfDay || JSONCache == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void cache(JSONStruc data)
        {
            JSONCache = data;
            JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5));
        }
    }
}