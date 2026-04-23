using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DRDownloadWindow2.Utilities
{
    public class ValueToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue)
            {
                if (double.TryParse(values[0]?.ToString(), out double percentage))
                {
                    return Util.PercentToCircleArcPoint(percentage);
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

