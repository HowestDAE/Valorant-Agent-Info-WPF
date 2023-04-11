using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PROJ_ValorantAgents.Model;
namespace PROJ_ValorantAgents
{
    internal class AgentsLocalRepository
    {
        private static List<Agent> _agents = new List<Agent>();
        public static List<Agent> GetAgents()
        {
            if(_agents.Count == 0)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var resourceName = "PROJ_ValorantAgents.Resources.Data.agents.json";

                using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream is null) throw new Exception("Failed to load embedded resource.");

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(json)) throw new Exception("Failed to read json from embedded resource.");

                        List<Agent>? agents = JsonConvert.DeserializeObject<List<Agent>>(json);
                        if (agents == null) throw new Exception("Failed to deserialize data from json.");

                        _agents = agents;
                        return agents;
                    }
                }
            } else { return _agents; }
        }

        public static Agent? GetAgent(string name)
        {
            if(_agents.Count == 0) GetAgents();

            return _agents.FirstOrDefault(agent => agent.displayName.ToLower() == name.ToLower());
        }
    }
}
