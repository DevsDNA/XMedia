using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XMedia.Converter
{
    [Preserve(AllMembers = true)]
    public class DateTimeLiteralValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            return dateTime.ToString("dd MMMM");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
