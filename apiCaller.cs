using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    internal class apiCaller
    {
        internal async Task<JSONStruc> mkHttpCall(string url)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("User-Agent", "ChobbyCodeUpdateChecker");

                //Passes the url to the http client and does stuff
                using (HttpResponseMessage response = await client.GetAsync(url))
                {

                    using (HttpContent content = response.Content)
                    {
                        // Converts json to JSONStruc
                        string httpResponse = await content.ReadAsStringAsync();
                        JSONStruc infoHttp = JsonConvert.DeserializeObject<JSONStruc>(httpResponse);

                        return infoHttp;


                    }
                }
            }
        }
    }
}
