
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RentIT.Converters
{
    class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Byte[] bytes = System.Convert.FromBase64String(value.ToString());
            //Creazione immagine
            Image img = new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };
            return img.Source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}