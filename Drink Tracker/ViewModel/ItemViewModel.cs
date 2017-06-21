using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        Ytem ytem;
        Item item;

        public ItemViewModel(Item i)
        {
            item = i;
            //createdText = Created.ToString("HH:mm:ss") + ", " + Created.ToString("dd.MM");
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

        public List<Ytem> Ytems
        {
            get { return item.Ytems; }
            set
            {
                item.Ytems = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime Added
        {
            //get { return Ytems.OrderByDescending(y => y.Added).First().Added; }
            // we need to get the current item.ytem's added here somehow and gg
            get { return ytem.Added; }
            set
            {
                ytem.Added = value;
                NotifyPropertyChanged();
            }
        }

        //public List<DateTime> Addeds
        //{
        //    get { return ytems.Added; }
        //    set
        //    {
        //        ytems.Added = value;
        //        NotifyPropertyChanged();
        //    }
       // }
    }
}
