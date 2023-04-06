using PROJ_ValorantAgents.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PROJ_ValorantAgents.View.Converters
{
    public class LocalAgentListItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<LocalAgent> agents)
            {
                var items = new List<ListBoxItem>();
                for (int i = 0; i < agents.Count; i++)
                {
                    var item = new ListBoxItem();
                    var sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;

                    var indexTextBlock = new TextBlock();
                    indexTextBlock.FontSize = 12;
                    indexTextBlock.Text = (i + 1).ToString() + ". ";
                    sp.Children.Add(indexTextBlock);

                    var nameTextBlock = new TextBlock();
                    nameTextBlock.FontSize = 75;
                    nameTextBlock.Text = agents[i].Name;
                    sp.Children.Add(nameTextBlock);

                    item.Content = sp;
                    items.Add(item);
                }
                return items;
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
