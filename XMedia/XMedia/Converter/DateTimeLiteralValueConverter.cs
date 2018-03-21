using System;
using System.Globalization;
using Xamarin.Forms;

namespace XMedia.Converter
{
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
