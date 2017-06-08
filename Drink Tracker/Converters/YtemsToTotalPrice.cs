using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Drink_Tracker.Converters
{
    class YtemsToTotalPrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ytems = value as List<Ytem>;
            return ytems.Count == 0 ? 0 : ytems.Count * ytems.First().Item.DrinkPrice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
