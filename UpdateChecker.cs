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

        public async Task<int> GetVersionPart(int part)
        {
            // Creates a new http client
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("User-Agent", "ChobbyCodeUpdateChecker");

                //Passes the url to the http client and does stuff
                using (HttpResponseMessage response = await client.GetAsync(@"https://api.github.com/repos/microsoft/artifacts-credprovider/releases/latest"))
                {

                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        JSONStruc info = JsonConvert.DeserializeObject<JSONStruc>(myContent);

                        string versionTag = info!.tag_name;


                        return 1;

                    }
                }
            }
        }
    }
}
