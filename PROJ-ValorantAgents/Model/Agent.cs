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
        //constructor
        public Agent()
        {
            abilities = new List<Ability>();
        }

        public string displayName { get; set; }
        public string description { get; set; }
        public object characterTags { get; set; }
        public string fullPortrait { get; set; }
        public string background { get; set; }
        public bool isPlayableCharacter { get; set; }
        public Role role { get; set; }
        public List<Ability> abilities { get; set; }
    }


    public class Role
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string displayIcon { get; set; }
        public string assetPath { get; set; }
    }

    public class Root
    {
        public List<Agent> data { get; set; }
    }
}
