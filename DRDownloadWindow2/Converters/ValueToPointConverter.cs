using DRDownloadLib.Utilities;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DRDownloadWindow.Converters
{
    public class ValueToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue)
            {
                if (double.TryParse(values[0]?.ToString(), out double percent))
                {
                    var coord = Util.PercentToCircleArcPoint(percent);
                    return new Point(coord.Item1, coord.Item2);
                }
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

