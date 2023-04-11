using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROJ_ValorantAgents.Model;

namespace PROJ_ValorantAgents.ViewModel
{
    internal class AgentOverviewVM : ObservableObject
    {
        public List<Agent>? Agents { get; set; }


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

        private string? _selectedRole;
        public string? SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                if (_selectedRole != null)
                {
                    AgentsApiRepository.GetAgentsByRoleAsync(_selectedRole).ContinueWith(task =>
                    {
                        Agents = task.Result;
                        OnPropertyChanged(nameof(Agents));
                    });
                    OnPropertyChanged(nameof(SelectedRole));
                }
            }
        }


        public ICommand FilterAgentsCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                FilterAgentsByName().ConfigureAwait(false);
                OnPropertyChanged(nameof(Agents));
            }
        }

        public AgentOverviewVM()
        {
            LoadAgentsAsync();

            FilterAgentsCommand = new RelayCommand<string>(async (role) => await FilterAgentsByRole(role));
            SearchCommand = new RelayCommand(async () => await FilterAgentsByName());
        }


        private async void LoadAgentsAsync()
        {
            Agents = await AgentsApiRepository.GetAgentsAsync();
            OnPropertyChanged(nameof(Agents));
        }

        public async Task FilterAgentsByRole(string role)
        {
            Agents = await AgentsApiRepository.GetAgentsByRoleAsync(role);
            OnPropertyChanged(nameof(Agents));
            SelectedRole = role;
        }

        public async Task FilterAgentsByName()
        {
            Agents = await AgentsApiRepository.GetAgentsByNameAsync(SearchText);
            OnPropertyChanged(nameof(SearchText));
        }
    }
}
