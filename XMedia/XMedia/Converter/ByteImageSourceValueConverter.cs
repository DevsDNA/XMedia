using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace XMedia.Converter
{
    public class ByteImageSourceValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] rawImage = (byte[])value;
            
            return ImageSource.FromStream(() => new MemoryStream(rawImage));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
