using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    public class UpdateChecker
    {
        public async Task<string> LatestVersionTag(string url)
        {
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

                        string versionTag = info!.tag_name;

                        //Returns the version
                        return versionTag;

                    }
                }
            }
        }

        public async Task<int> GetVersionPart(int part, string url)
        {
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
                        JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);

                        string versionTag = info!.tag_name + ".";

                        string letter = "";
                        string word = "";
                        int charLet = 1;
                        while(letter != ".")
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

                        if(part == 0)
                        {
                            try
                            {
                                return Convert.ToInt16(MainVersion);
                            }
                            catch
                            {
                                return -1;
                            }
                        }else if (part == 1)
                        {
                            try
                            {
                                return Convert.ToInt16(MinorVersion);
                            }
                            catch
                            {
                                return -1;
                            }
                        }else if (part == 2)
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

        public async Task<bool> CheckForUpdates(string currentVersion, string url)
        {
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

                        string versionTag = info!.tag_name;

                        //Returns the version
                        return versionTag;

                    }
                }
            }
        }
    }
}
