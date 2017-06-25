using System;
using Windows.UI.Xaml.Data;

namespace Drink_Tracker.Converters
{
    class BoolToExpandedIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool boolean = (bool)value;
            return boolean ? "\uE70E" : "\uE70D";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
