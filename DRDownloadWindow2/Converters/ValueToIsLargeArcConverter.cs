using DRDownloadWindow2.Utilities;
using System.Globalization;
using System.Windows.Data;

namespace DRDownloadWindow2.Converters
{
    public class ValueToIsLargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != System.Windows.DependencyProperty.UnsetValue)
            {
                if (double.TryParse(values[0]?.ToString(), out double percent))
                {
                    return Util.PercentToCircleIsLargeArc(percent);
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

