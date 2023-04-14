using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PROJ_ValorantAgents.View.Converters
{
    public class BooleanToPathDataConverter : IValueConverter
    {
        public Geometry TruePathData { get; set; }
        public Geometry FalsePathData { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue && booleanValue)
            {
                return TruePathData;
            }

            return FalsePathData;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
