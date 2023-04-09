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
        private static List<LocalAgent> localAgents = new List<LocalAgent>();
        public static List<LocalAgent> GetLocalAgents()
        {
            if(localAgents.Count == 0)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var resourceName = "PROJ_ValorantAgents.Resources.Data.agents.json";

                var resourceNames = assembly.GetManifestResourceNames();

                foreach (var name in resourceNames)
                {
                    Console.WriteLine(name);
                }

                using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream is null) throw new Exception("Failed to load embedded resource.");

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(json)) throw new Exception("Failed to read json from embedded resource.");

                        List<LocalAgent>? agents = JsonConvert.DeserializeObject<List<LocalAgent>>(json);
                        if (agents == null) throw new Exception("Failed to deserialize data from json.");

                        localAgents = agents;
                        return agents;
                    }
                }
            } else { return localAgents; }
        }
    }
}
