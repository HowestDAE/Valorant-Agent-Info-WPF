using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PROJ_ValorantAgents.Model;

namespace PROJ_ValorantAgents.ViewModel
{
    internal class AgentOverviewVM : ObservableObject
    {
        public List<Agent> Agents { get; set; }
        public List<LocalAgent> LocalAgents { get; set; }

        private Agent _selectedAgent = new Agent();
        public Agent SelectedAgent
        {
            get { return _selectedAgent; }
            set
            {
                _selectedAgent = value;
                OnPropertyChanged(nameof(SelectedAgent));
            }
        }

        public AgentOverviewVM()
        {
            LocalAgents = AgentsLocalRepository.GetLocalAgents();
            OnPropertyChanged(nameof(LocalAgents));

            LoadAgentsAsync();
        }

        private async void LoadAgentsAsync()
        {
            Agents = await AgentsApiRepository.GetAgentsAsync();
            OnPropertyChanged(nameof(Agents));
        }
    }
}
