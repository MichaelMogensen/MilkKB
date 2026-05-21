using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace DRDownloadWindow.Converters
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

                return CanDownload(progress, entryId);
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calc. can download.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public static bool CanDownload(int progress, string entryId)
        {
            var canDownload = progress == 0 && entryId == null;

            //var notInProgress = progress == 0;

            //var regEx = new Regex(Const.REGEX_PATTERN_ENTRY_ID);
            //var holdsEntryId = entryId == null || regEx.Match(entryId).Success;

            //var canDownload = !notInProgress && !holdsEntryId;

            return canDownload;
        }
    }
}

