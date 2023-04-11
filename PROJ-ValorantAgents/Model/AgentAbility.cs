using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_ValorantAgents.Model
{
    internal class AgentAbility
    {
        public string name { get; set; }
        public Dictionary<string, string> stats { get; set; }
        public Dictionary<string, string> photos { get; set; }
        public Dictionary<string, string> videos { get; set; }
        public Dictionary<string, string> descriptions { get; set; }
        public Dictionary<string, string> costs { get; set; }
        public Dictionary<string, string> charges { get; set; }
    }
}
