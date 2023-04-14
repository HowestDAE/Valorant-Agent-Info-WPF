using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_ValorantAgents.Model
{
    public class BaseAgent
    {
        public virtual string Name { get; set; } = "Reyna";
        public string Biography { get; set; }
          = "Forged in the heart of Mexico, Reyna dominates single combat, " +
            "popping off with each kill she scores. Her capability is only limited by her raw skill, " +
            "making her sharply dependant on performance.";

        public Role Role { get; set; } = new Role();
        public List<Ability> Abilities { get; set; } = new List<Ability>();
    }

    public class Agent
    {
        public string uuid { get; set; } = "a3bfb853-43b2-7238-a4f1-ad90e9e46bcc";
        public string displayName { get; set; } = "Reyna";
        public string description { get; set; }
            = "Forged in the heart of Mexico, Reyna dominates single combat, " +
            "popping off with each kill she scores. Her capability is only limited by her raw skill, " +
            "making her sharply dependant on performance.";
        public string fullPortrait => $"https://media.valorant-api.com/agents/{uuid}/fullportrait.png";
        public string background => $"https://media.valorant-api.com/agents/{uuid}/background.png";
        public bool isPlayableCharacter { get; set; }
        public Role role { get; set; } = new Role();
        public List<Ability> abilities { get; set; } = new List<Ability>();

        public override string ToString()
        {
            return displayName.ToUpper();
        }
    }

    public class Role
    {
        public string displayName { get; set; } = "INITIATOR";
        public string? displayIcon { get; set; } = "https://media.valorant-api.com/agents/roles/dbe8757e-9e92-4ed4-b39f-9dfc589691d4/displayicon.png";
    }

    [JsonObject]
    public class AgentListWrapper
    {
        [JsonProperty("data")]
        public List<Agent>? Data { get; set; }
    }



}
