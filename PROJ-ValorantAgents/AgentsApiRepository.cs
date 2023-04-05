using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PROJ_ValorantAgents.Model;

namespace PROJ_ValorantAgents
{
    internal class AgentsApiRepository
    {
        private static List<Agent> _agents;
        public static List<Agent> Agents
        {
            get { return _agents; }
        }
        
        public static async Task LoadAgentsAsync()
        {
            const string url = "https://valorant-api.com/v1/agents";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if(!response.IsSuccessStatusCode) throw new HttpRequestException(response.ReasonPhrase);

                string json = await response.Content.ReadAsStringAsync();

                // deserialize json
                var serializedData = JsonConvert.DeserializeObject<Root>(json);

                if (serializedData == null) throw new Exception("Failed to deserialize agents.");

                _agents = serializedData.data;
            }
        }
    }
}
