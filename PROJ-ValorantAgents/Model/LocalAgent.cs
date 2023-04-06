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
        private string _name;
        public string Name
        {
            get { return _name.ToUpper(); }
            set { _name = value; }
        }
        public Biography Biography { get; set; }

        //override tostring
        public override string ToString()
        {
            return Name.ToUpper();
        }
    }
}
