using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PROJ_ValorantAgents.Model
{
    public class Ability
    {
        public Ability() { }
        public Ability(string _slot)
        {
            slot = _slot;
        }
        public string agentName { get; set; } = "Reyna";
        public string slot { get; set; } = "Q";
        public string displayName { get; set; } = "Soul Harvest";
        public string description { get; set; } = "Enemies that die to Reyna, or die within 3 seconds of taking damage from Reyna, leave behind Soul Orbs that last 3 seconds";
        public string displayIcon { get; set; } = "https://media.valorant-api.com/agents/a3bfb853-43b2-7238-a4f1-ad90e9e46bcc/abilities/ability1/displayicon.png";
        public string? tutorialVideo { get; set; }
        public string? cost { get; set; }
        public string? charge { get; set; }
    }
}
