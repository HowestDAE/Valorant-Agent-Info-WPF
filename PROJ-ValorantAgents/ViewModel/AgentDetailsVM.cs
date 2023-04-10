using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROJ_ValorantAgents.Model;

namespace PROJ_ValorantAgents.ViewModel
{
    internal class AgentDetailsVM : ObservableObject
    {
        private Agent _currentAgent = new Agent();
        public Agent CurrentAgent
        { 
            get { return _currentAgent; } 
            set
            {
                _currentAgent = value;
                OnPropertyChanged(nameof(CurrentAgent));
            }
        }

        private Ability _currentAbility = new Ability();
        public Ability CurrentAbility
        {
            get { return _currentAbility; }
            set
            {
                _currentAbility = value;
                OnPropertyChanged(nameof(CurrentAbility));
            }
        }

        public RelayCommand<Ability> SelectAbilityCommand { get; set; }
        public void SelectAbility(Ability ability)
        {
            CurrentAbility = ability;
            OnPropertyChanged(nameof(CurrentAbility));
        }

        public AgentDetailsVM()
        {
            SelectAbilityCommand = new RelayCommand<Ability>((parameter) => SelectAbility(parameter));
        }


    }
}
