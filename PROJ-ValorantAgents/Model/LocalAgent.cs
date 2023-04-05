using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_ValorantAgents.Model
{
    public class Biography
    {
        public string story { get; set; }
        public string agent_about { get; set; }
    }

    public class LocalAgent
    {
        public string name { get; set; }
        public Biography biography { get; set; }
    }
}
