using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PROJ_ValorantAgents.Model;

namespace PROJ_ValorantAgents.ViewModel
{
    internal class AgentDetailsVM : ObservableObject
    {
        private Agent _currentAgent;
        public Agent CurrentAgent
        { 
            get { return _currentAgent; } 
            set
            {
                _currentAgent = value;
                OnPropertyChanged(nameof(CurrentAgent));
            }
        }
    }
}
