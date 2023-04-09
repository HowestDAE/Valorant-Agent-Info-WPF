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
        private static List<Agent> agents = new List<Agent>();
        
        public static async Task<List<Agent>> GetAgentsAsync()
        {
            if(agents.Count == 0)
            {
                const string url = "https://valorant-api.com/v1/agents";

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string json = await content.ReadAsStringAsync();
                            AgentListWrapper agentsList = JsonConvert.DeserializeObject<AgentListWrapper>(json);
                            if(agentsList == null) throw new Exception("Failed to deserialize data from json.");

                            agents = agentsList.Data;

                            //remove agents that aren't playablecharacters
                            agents.RemoveAll(agent => !agent.isPlayableCharacter);

                            return agents;
                        }
                    }
                }
            } else { return agents; }
        }

        public static async Task<List<Agent>> GetAgentsByRoleAsync(string role)
        {
            if (agents.Count == 0)
            {
                agents = await GetAgentsAsync();
            }

            if(role.ToLower() == "all") return agents;
            else return agents.Where(agent => agent.role.displayName.ToLower() == role.ToLower()).ToList();
        }


        public static async Task<List<Agent>> GetAgentsByNameAsync(string? name)
        {
            if (agents.Count == 0)
            {
                agents = await GetAgentsAsync();
            }


            if (string.IsNullOrWhiteSpace(name) || name.ToUpper() == "SEARCH") return agents; 
            else
            {
                string searchName = name.Trim(); // remove leading/trailing spaces from name
                return agents.Where(agent => agent.displayName.ToLower().Contains(searchName.ToLower())).ToList();
            }
        }
    }
}
