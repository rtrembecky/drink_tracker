using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class YtemViewModel : ViewModelBase
    {
        Ytem ytem;
        Item item;

        public YtemViewModel(Ytem y)
        {
            ytem = y;
            //createdText = Created.ToString("HH:mm:ss") + ", " + Created.ToString("dd.MM");
        }

        public Ytem Ytem
        {
            get { return ytem; }
        }

        public DateTime Added
        {
            get { return ytem.Added; }
            set
            {
                ytem.Added = value;
                NotifyPropertyChanged();
            }
        }

        public Item Item
        {
            get { return item; }
        }

        public float ItemPrice
        {
            get { return item.DrinkPrice; }
        }

        public String ItemName
        {
            get { return item.Drink.Name; }
        }
    }
}
