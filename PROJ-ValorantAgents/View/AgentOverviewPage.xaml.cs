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
        List<Button> _navigationButtons = new List<Button>();
        public AgentOverviewPage()
        {
            InitializeComponent();

            Button allBtn = (Button)FindName("ALL");
            Button controllersBtn = (Button)FindName("CONTROLLERS");
            Button duelistsBtn = (Button)FindName("DUELISTS");
            Button initiatorsBtn = (Button)FindName("INITIATORS");
            Button sentinelsBtn = (Button)FindName("SENTINELS");

            allBtn.Foreground = (SolidColorBrush)FindResource("Red"); // Set the default selection

            _navigationButtons.Add(allBtn);
            _navigationButtons.Add(controllersBtn);
            _navigationButtons.Add(duelistsBtn);
            _navigationButtons.Add(initiatorsBtn);
            _navigationButtons.Add(sentinelsBtn);
        }

        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            TextBox search = (TextBox)FindName("searchBox");
            search.Text = string.Empty;
            // Get the search box and close search button elements
            FrameworkElement searchBox = (FrameworkElement)FindName("searchBox");
            FrameworkElement closeSearchButton = (FrameworkElement)FindName("CloseSearchButton");

            // Create an instance of the storyboard and start it
            Storyboard storyboard = (Storyboard)FindResource("HideSearchBox");
            storyboard.Begin(searchBox);
            storyboard.Begin(closeSearchButton);


            Button clickedButton = (Button)sender;

            foreach (Button button in _navigationButtons)
                if (button == clickedButton)
                    button.Foreground = (SolidColorBrush)FindResource("Red");
                 else
                    button.Foreground = (SolidColorBrush)FindResource("OffWhite");
        }

        private void CloseSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is AgentOverviewVM agentOverviewVM)
            {
                agentOverviewVM.SearchText = string.Empty;
            }
        }

        private void AgentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // cast sender to listbox
            ListBox listBox = (ListBox)sender;

            if(listBox.SelectedIndex == -1)
            listBox.SelectedIndex = 0;
        }
    }
}
