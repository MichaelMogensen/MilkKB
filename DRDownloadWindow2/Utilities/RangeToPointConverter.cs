using System.Globalization;
using System.Windows.Data;

namespace DRDownloadWindow2.Utilities
{
    public class RangeToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = (float)values[0];

            return Util.PointAtCircle(percentage);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

