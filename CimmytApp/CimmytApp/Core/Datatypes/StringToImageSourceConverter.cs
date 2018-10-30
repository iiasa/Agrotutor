namespace CimmytApp.Core.Datatypes
{
    using System;
    using System.Globalization;
    using Microsoft.AppCenter;
    using Xamarin.Forms;

    public class StringToImageSourceConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;
            try
            {
                string path = (string)value;
                imageSource = ImageSource.FromFile(path);
            }
            catch (Exception e)
            {
                AppCenterLog.Error("StringToImageSourceConverter", "Convert failed", e);
            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ImageSource source ? source.ToString() : null;
        }
    }
}
