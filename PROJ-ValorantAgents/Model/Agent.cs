using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_ValorantAgents.Model
{
    public class Ability
    {
        public string slot { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string displayIcon { get; set; }
    }

    public class Agent
    {
        public string displayName { get; set; } = "Reyna";
        public string description { get; set; } = "Forged in the heart of Mexico, Reyna dominates single combat, popping off with each kill she scores. Her capability is only limited by her raw skill, making her sharply dependant on performance.";
       // public object characterTags { get; set; }
        public string fullPortrait { get; set; } = "https://media.valorant-api.com/agents/a3bfb853-43b2-7238-a4f1-ad90e9e46bcc/fullportrait.png";
        public string background { get; set; } = "https://media.valorant-api.com/agents/a3bfb853-43b2-7238-a4f1-ad90e9e46bcc/background.png";
        public bool isPlayableCharacter { get; set; }
        public Role role { get; set; } = new Role();
        public List<Ability> abilities { get; set; }

        // override tostring
        public override string ToString()
        {
            return displayName.ToUpper();
        }
    }

    public class Role
    {
        public string displayName { get; set; } = "INITIATOR";
        public string description { get; set; }
        public string displayIcon { get; set; } = "https://media.valorant-api.com/agents/roles/dbe8757e-9e92-4ed4-b39f-9dfc589691d4/displayicon.png";
        public string assetPath { get; set; }
    }

    [JsonObject]
    public class AgentListWrapper
    {
        [JsonProperty("data")]
        public List<Agent> Data { get; set; }
    }



}
