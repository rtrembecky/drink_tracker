using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Drink_Tracker.Converters
{
    class BoolToNonVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool boolean = (bool)value;
            return boolean ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
