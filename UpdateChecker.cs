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
        public string OWNERNAME = null;
        public string REPONAME = null;
        public string VERSION = null;

        //Cache the JSON to minimise the amount of API calls to the github api so that no get sued or something like that :)
        private string JSONCache = "";
        private DateTime JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5));


        public async Task<string> LatestVersionTag()
        {
            if (OWNERNAME == null || REPONAME == null)
            {
                return "ERROR: Creds Not Setup. Read the documentation at https://github.com/ChobbyCode/Update-Checker";
            }
            else
            {
                if(JSONCacheAge.TimeOfDay < DateTime.Now.TimeOfDay || JSONCache == "")
                {
                    //We can recache

                    //Sets the url
                    string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";

                    // Creates a new http client
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Add("User-Agent", "ChobbyCodeUpdateChecker");

                        //Passes the url to the http client and does stuff
                        using (HttpResponseMessage response = await client.GetAsync(url))
                        {

                            using (HttpContent content = response.Content)
                            {
                                // Reads
                                string myContent = await content.ReadAsStringAsync();
                                JSONCache = myContent; // Sets the cache
                                JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5)); // Sets cache age
                                JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);

                                string versionTag = info!.tag_name;

                                //Returns the version
                                return versionTag;

                            }
                        }
                    }
                }
                else
                {
                    //No point in recaching
                    string myContent = JSONCache;
                    JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);
                    
                    string versionTag = info!.tag_name;

                    //Returns the version
                    return versionTag;
                }
            }
        }

        public async Task<int> GetVersionPart(int part)
        {

            if (JSONCacheAge.TimeOfDay < DateTime.Now.TimeOfDay || JSONCache == "")
            {

                if (OWNERNAME == null || REPONAME == null)
                {
                    return -1;
                }
                else
                {

                    string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";


                    // Creates a new http client
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Add("User-Agent", "ChobbyCodeUpdateChecker");

                        //Passes the url to the http client and does stuff
                        using (HttpResponseMessage response = await client.GetAsync(url))
                        {

                            using (HttpContent content = response.Content)
                            {
                                string myContent = await content.ReadAsStringAsync();
                                JSONCache = myContent; // Sets the cache
                                JSONCacheAge = DateTime.Now.Add(TimeSpan.FromMinutes(5)); // Sets cache age
                                JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);

                                string versionTag = info!.tag_name + ".";

                                string letter = "";
                                string word = "";
                                int charLet = 1;
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                string MainVersion = word;

                                word = "";
                                letter = "";
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                string MinorVersion = word;

                                word = "";
                                letter = "";
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                string BuildVersion = word;

                                if (part == 0)
                                {
                                    try
                                    {
                                        return Convert.ToInt16(MainVersion);
                                    }
                                    catch
                                    {
                                        return -1;
                                    }
                                }
                                else if (part == 1)
                                {
                                    try
                                    {
                                        return Convert.ToInt16(MinorVersion);
                                    }
                                    catch
                                    {
                                        return -1;
                                    }
                                }
                                else if (part == 2)
                                {
                                    try
                                    {
                                        return Convert.ToInt16(BuildVersion); ;
                                    }
                                    catch
                                    {
                                        return -1;
                                    }
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                        }
                    }
                }
            }
            else
            {

                JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(JSONCache);

                string versionTag = info!.tag_name + ".";

                string letter = "";
                string word = "";
                int charLet = 1;
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                string MainVersion = word;

                word = "";
                letter = "";
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                string MinorVersion = word;

                word = "";
                letter = "";
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                string BuildVersion = word;

                if (part == 0)
                {
                    try
                    {
                        return Convert.ToInt16(MainVersion);
                    }
                    catch
                    {
                        return -1;
                    }
                }
                else if (part == 1)
                {
                    try
                    {
                        return Convert.ToInt16(MinorVersion);
                    }
                    catch
                    {
                        return -1;
                    }
                }
                else if (part == 2)
                {
                    try
                    {
                        return Convert.ToInt16(BuildVersion); ;
                    }
                    catch
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }


        public async Task<bool> CheckForUpdates(string currentVersionTag)
        {
            if(currentVersionTag == null)
            {
                return false;
            }

            /// <summary>
            /// Makes a http request to url does some magic and returns true if an update is available and false if there is not. Check documentation at https://github.com/ChobbyCode/Update-Checker
            /// </summary>
            /// <param name="currentVersion"></param>
            /// <param name="url"></param>
            /// <returns></returns>
            /// 
            string currentVersion = currentVersionTag + "."; // Adds a dot to the end to prevent out of range error

            // Parse the current version into chunks

            string letter = "";
            string word = "";
            int charLet = 1;
            while (letter != ".")
            {
                word = word + letter;

                letter = currentVersion[charLet].ToString();
                charLet += 1;
            }
            int CurrentMainVersion = Convert.ToInt16(word);

            word = "";
            letter = "";
            while (letter != ".")
            {
                word = word + letter;

                letter = currentVersion[charLet].ToString();
                charLet += 1;
            }
            int CurrentMinorVersion = Convert.ToInt16(word);

            word = "";
            letter = "";
            while (letter != ".")
            {
                word = word + letter;

                letter = currentVersion[charLet].ToString();
                charLet += 1;
            }
            int CurrentBuildVersion = Convert.ToInt16(word);


            if (JSONCacheAge.TimeOfDay < DateTime.Now.TimeOfDay || JSONCache == "")
            {

                if (OWNERNAME == null || REPONAME == null)
                {
                    return false;
                }
                else
                {
                    string url = @"https://api.github.com/repos/" + OWNERNAME + "/" + REPONAME + "/releases/latest";


                    // Creates a new http client
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Add("User-Agent", "ChobbyCodeUpdateChecker");

                        //Passes the url to the http client and does stuff
                        using (HttpResponseMessage response = await client.GetAsync(url))
                        {

                            using (HttpContent content = response.Content)
                            {
                                // Reads
                                string myContent = await content.ReadAsStringAsync();
                                JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);


                                // Gets the latest version
                                string versionTag = info!.tag_name + ".";

                                // Parse the latest version as above

                                letter = "";
                                word = "";
                                charLet = 1;
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                int LatestMainVersion = Convert.ToInt16(word);

                                word = "";
                                letter = "";
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                int LatestMinorVersion = Convert.ToInt16(word);

                                word = "";
                                letter = "";
                                while (letter != ".")
                                {
                                    word = word + letter;

                                    letter = versionTag[charLet].ToString();
                                    charLet += 1;
                                }
                                int LatestBuildVersion = Convert.ToInt16(word);

                                // Compares the values
                                if (LatestMainVersion > CurrentMainVersion)
                                {
                                    return true;
                                }
                                else if (LatestMinorVersion > CurrentMinorVersion)
                                {
                                    return true;
                                }
                                else if (LatestBuildVersion > CurrentBuildVersion)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                // Reads
                string myContent = JSONCache;
                JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);


                // Gets the latest version
                string versionTag = info!.tag_name + ".";

                // Parse the latest version as above

                letter = "";
                word = "";
                charLet = 1;
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                int LatestMainVersion = Convert.ToInt16(word);

                word = "";
                letter = "";
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                int LatestMinorVersion = Convert.ToInt16(word);

                word = "";
                letter = "";
                while (letter != ".")
                {
                    word = word + letter;

                    letter = versionTag[charLet].ToString();
                    charLet += 1;
                }
                int LatestBuildVersion = Convert.ToInt16(word);

                // Compares the values
                if (LatestMainVersion > CurrentMainVersion)
                {
                    return true;
                }
                else if (LatestMinorVersion > CurrentMinorVersion)
                {
                    return true;
                }
                else if (LatestBuildVersion > CurrentBuildVersion)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
