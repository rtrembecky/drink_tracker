using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Drink_Tracker.Converters
{
    class YtemsToCount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ytems = value as List<Ytem>;
            return ytems.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
