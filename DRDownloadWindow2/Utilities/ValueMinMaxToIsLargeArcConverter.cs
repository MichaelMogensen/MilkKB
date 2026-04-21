using System.Globalization;
using System.Windows.Data;

namespace DRDownloadWindow2.Utilities
{
    public class ValueMinMaxToIsLargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = (double)values[0];
            var min = (double)values[1];
            var max = (double)values[2];

            var range = max - min;

            // Is bigger than 50%?
            var isBiggerThanHalf = value >= range / 2;

            return isBiggerThanHalf;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

