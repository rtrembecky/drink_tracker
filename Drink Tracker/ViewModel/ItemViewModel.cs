using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
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

        public DateTime LastAdded
        {
            get { return ytems.OrderByDescending(y => y.Added).First().Added; }
        }

        //delete this
        public List<Ytem> Ytemsx
        {
            get { return item.Ytems; }
            set
            {
                item.Ytems = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<YtemViewModel> ytems;
        public ObservableCollection<YtemViewModel> Ytems
        {
            get { return ytems; }
            set
            {
                ytems = value;
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
