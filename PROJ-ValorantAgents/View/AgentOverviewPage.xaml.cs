using PROJ_ValorantAgents.Model;
using PROJ_ValorantAgents.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROJ_ValorantAgents.View
{
    /// <summary>
    /// Interaction logic for AgentOverviewPage.xaml
    /// </summary>
    public partial class AgentOverviewPage : Page
    {
        public AgentOverviewPage()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(DataContext is AgentOverviewVM vm)
            {
                if(vm.SearchText.ToUpper() == "SEARCH")
                {
                    vm.SearchText = string.Empty;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(DataContext is AgentOverviewVM vm)
            {
                if(string.IsNullOrEmpty(vm.SearchText))
                {
                    vm.SearchText = "SEARCH";
                }
            }
        }
    }
}
