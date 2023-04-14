using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PROJ_ValorantAgents.Model;
using static PROJ_ValorantAgents.Model.AgentAbility;

namespace PROJ_ValorantAgents
{
    internal class AgentsApiRepository
    {
        private static List<Agent> agents = new List<Agent>();
        
        public static async Task<List<Agent>> GetAgentsAsync()
        {
            if(agents.Count == 0)
            {
                List<Agent> tmp_agents = new List<Agent>();

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

                            tmp_agents = agentsList.Data;

                            //remove agents that aren't playablecharacters
                            tmp_agents?.RemoveAll(agent => !agent.isPlayableCharacter);

                        }
                    }
                }


                // Deserialize useful ability info from different json (local file)
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "PROJ_ValorantAgents.Resources.Data.agents.json";

                List<AgentAbility> abilities = new List<AgentAbility>();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();

                    abilities = JsonConvert.DeserializeObject<List<AgentAbility>>(json);

                    if (abilities == null) throw new Exception("Failed to deserialize data from json.");

                    foreach(var agent in tmp_agents)
                    {
                        var agentAbilities = abilities.Where(c => c.name.ToLower() == agent.displayName.ToLower()).FirstOrDefault();
                        if(agentAbilities == null) continue;

                        foreach(Ability ability in agent.abilities)
                        {
                            if(ability.slot != "Passive") SetAbilityData(ability, agentAbilities); // we have no data for passives
                        }
                    }
                }


                agents = tmp_agents;
                return agents;

            } else { return agents; }
        }
        public static void SetAbilityData(Ability ability, AgentAbility data)
        {
            ConvertSlotToKey(ability);

            ability.cost = data.costs.First(pair => pair.Key.StartsWith(ability.slot)).Value;
            ability.tutorialVideo = data.videos.First(pair => pair.Key.StartsWith(ability.slot)).Value;
            ability.charge = data.charges.First(pair => pair.Key.StartsWith(ability.slot)).Value;

            // i think i prefer the descriptions and icons from the remote json
            //ability.description = data.descriptions.First(pair => pair.Key.StartsWith(ability.slot)).Value;
            //ability.displayIcon = data.photos.First(pair => pair.Key.StartsWith(ability.slot)).Value;
        }

        public static void ConvertSlotToKey(Ability ability)
        {
            switch(ability.slot)
            {
                case "Ability1":
                    ability.slot = "q";
                    break;

                case "Ability2":
                    ability.slot = "c";
                    break;

                case "Grenade":
                    ability.slot = "e";
                    break;

                case "Ultimate":
                    ability.slot = "x";
                    break;

                case "Passive":
                    ability.slot = "p";
                    break;

                default:
                    throw new ArgumentException("Invalid slot name: " + ability.slot);
            }
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

        public static async Task<List<Agent>> GetFilteredAgents(string role, string name)
        {
            if(agents.Count == 0)
            {
                agents = await GetAgentsAsync();
            }

            // filter agents by role
            List<Agent> filteredAgents = agents.Where(agent => agent.role.displayName.ToLower() == role.ToLower()).ToList();

            // filter agents by name
            return filteredAgents.Where(agent => agent.displayName.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
