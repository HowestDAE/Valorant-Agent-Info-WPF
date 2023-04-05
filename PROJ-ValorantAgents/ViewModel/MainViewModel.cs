using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PROJ_ValorantAgents.Model;
using PROJ_ValorantAgents.View;

namespace PROJ_ValorantAgents.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public AgentsApiRepository AgentsApiRepository { get; set; } = new AgentsApiRepository();
        public AgentsLocalRepository AgentsLocalRepository { get; set; } = new AgentsLocalRepository();


        public AgentOverviewPage AgentOverview { get; set; } = new AgentOverviewPage();
        public AgentDetailsPage AgentDetails { get; set; } = new AgentDetailsPage();

        public Page CurrentPage { get; set; } = new AgentOverviewPage();

        public RelayCommand SwitchPageCommand { get; set; }

        public void SwitchPage()
        {
            if (CurrentPage is AgentOverviewVM)
            {
                Agent selectedAgent = (CurrentPage.DataContext as AgentOverviewVM).SelectedAgent;

                if (selectedAgent == null) return;

                (AgentDetails.DataContext as AgentDetailsVM).CurrentAgent = selectedAgent;

                CurrentPage = AgentDetails;
                OnPropertyChanged(nameof(CurrentPage));
            }
            else
            {
                CurrentPage = AgentOverview; 
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
    }
}
