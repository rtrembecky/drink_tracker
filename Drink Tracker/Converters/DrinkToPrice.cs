using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Drink_Tracker.Converters
{
    public class DrinkToPrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Drink drink = value as Drink;
            return drink.PriceInKc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
