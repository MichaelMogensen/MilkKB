using DRDownloadWindow2.DRBroadcast;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace DRDownloadWindow2.Converters
{
    public class CanDownloadConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue &&
                values[1] != DependencyProperty.UnsetValue)
            {
                var progress = (int)values[0];
                var entryId = (string)values[1];

                var notInProgress = progress == 0;

                var regEx = new Regex(DRBroadcastHtmlScraper.ENTRY_ID_PATTERN);
                var holdsEntryId = regEx.Match(entryId).Success;


                var canDownload = notInProgress && holdsEntryId;

                return canDownload;
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

