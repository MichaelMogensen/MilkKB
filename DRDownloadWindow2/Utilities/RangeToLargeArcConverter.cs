using System.Globalization;
using System.Windows.Data;

namespace DRDownloadWindow2.Utilities
{
    public class RangeToLargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = (float)values[0];

            return value >= 50f;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

