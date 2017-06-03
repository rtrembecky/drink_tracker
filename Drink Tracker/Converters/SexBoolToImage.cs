using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Drink_Tracker.Converters
{
    public class SexBoolToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string man = "ms-appx:///Images/man.jpg";
            string woman = "ms-appx:///Images/woman.jpg";
            var manUri = new Uri(man);
            var womanUri = new Uri(woman);
            BitmapImage manImage = new BitmapImage();
            BitmapImage womanImage = new BitmapImage();
            manImage.UriSource = manUri;
            womanImage.UriSource = womanUri;
            return (bool)value ? manImage : womanImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
