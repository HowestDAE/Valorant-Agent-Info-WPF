using System;
using System.Collections.Generic;
using System.Data;
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
        private List<Agent>? _agents;

        public List<Agent>? Agents
        {
            get { return _agents; }
            set
            {
                _agents = value;
                OnPropertyChanged(nameof(Agents));
            }
        }

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
                    OnPropertyChanged(nameof(SelectedRole));
                }
            }
        }

        public RelayCommand SwitchRepoCommand { get; private set; }
        public RelayCommand<string> FilterAgentsCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }

        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                SearchCommand?.Execute(value);
            }
        }

        public bool IsSearchOpen { get; set; } = false;

        private bool _isUsingAPI = true;
        public bool IsUsingAPI
        {
            get { return _isUsingAPI; }
            set
            {
                _isUsingAPI = value;
                OnPropertyChanged(nameof(IsUsingAPI));
            }
        }

        public AgentOverviewVM()
        {
            LoadAgentsAsync();

            FilterAgentsCommand = new RelayCommand<string>(async (role) => await FilterAgentsByRoleAsync(role));
            SearchCommand = new RelayCommand(async () => await FilterAgentsByNameAsync());
            SwitchRepoCommand = new RelayCommand(() => SwitchRepo());
        }

        private void LoadAgents()
        {
            Agents = AgentsLocalRepository.GetAgents();
        }

        private async void LoadAgentsAsync()
        {
            Agents = await AgentsApiRepository.GetAgentsAsync();
        }

        public void FilterAgentsByRole(string role)
        {
            Agents = AgentsLocalRepository.GetAgentsByRole(role);
            SelectedRole = role;
        }

        public async Task FilterAgentsByRoleAsync(string role)
        {
            Agents = await AgentsApiRepository.GetAgentsByRoleAsync(role);
            SelectedRole = role;
        }

        public void FilterAgentsByName()
        {
            Agents = AgentsLocalRepository.GetAgentsByName(SearchText);

            if(SelectedRole != null && SelectedRole != string.Empty && SelectedRole.ToUpper() != "ALL")
            {
                Agents = Agents.Where(agent => agent.role.displayName == SelectedRole).ToList();
            }
        }

        public async Task FilterAgentsByNameAsync()
        {
            Agents = await AgentsApiRepository.GetAgentsByNameAsync(SearchText);

            //remove agents with wrong role
            if (SelectedRole != null && SelectedRole != string.Empty && SelectedRole.ToUpper() != "ALL")
            {
                Agents = Agents.Where(agent => agent.role.displayName == SelectedRole).ToList();
            }
        }

        public void SwitchRepo()
        {
            IsUsingAPI = !IsUsingAPI;
            if (IsUsingAPI)
            {
                FilterAgentsCommand = new RelayCommand<string>(role => FilterAgentsByRole(role));
                SearchCommand = new RelayCommand(() => FilterAgentsByName());
            }
            else
            {
                FilterAgentsCommand = new RelayCommand<string>(async (role) => await FilterAgentsByRoleAsync(role));
                SearchCommand = new RelayCommand(async () => await FilterAgentsByNameAsync());
            }
        }
    }
}