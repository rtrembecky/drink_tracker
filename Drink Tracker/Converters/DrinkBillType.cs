using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.Converters
{
    public class DrinkBillType
    {
        public Drink Drink { get; set; }
        public BillAndType BillAndType { get; set; }

        public DrinkBillType(Drink drink, BillAndType billAndType)
        {
            Drink = drink;
            BillAndType = billAndType;
        }
    }
}
