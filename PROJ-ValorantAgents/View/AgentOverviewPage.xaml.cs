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
using System.Windows.Media.Animation;
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

        private void OnOpenSearchBox(object sender, RoutedEventArgs e)
        {
            Button? closeButton = FindName("CloseSearchButton") as Button;

            if (closeButton != null)
            {
                closeButton.Width = double.NaN; // Set the Width to "auto"
            }
        }

        private void CloseSearchBox(object sender, RoutedEventArgs e)
        {
            Button? closeButton = sender as Button;
            if (closeButton != null)
            {
                closeButton.Width = 0; 
            }
        }
    }
}
