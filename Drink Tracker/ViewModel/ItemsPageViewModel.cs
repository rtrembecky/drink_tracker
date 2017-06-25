using Drink_Tracker.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace Drink_Tracker.ViewModel
{
    public class ItemsPageViewModel : ViewModelBase
    {
        float totalPrice;

        public ItemsPageViewModel(Bill bill)
        {
            DatabaseManager manager = new DatabaseManager();
            bill.Items = manager.GetItemsFullByBill(bill);
            itemsList = new ObservableCollection<ItemViewModel>(bill.Items.Select(item => new ItemViewModel(item)));

            itemsTitleText = bill.Name + " bill";

            Calculation(bill);

            toPayText = "To pay: " + totalPrice.ToString("0.00") + " CZK";
        }

        private void Calculation(Bill b)
        {
            totalPrice = (float)0;
            foreach (var i in b.Items)
            {
                totalPrice += i.DrinkPrice * i.Timestamps.Count;
            }
        }

        string itemsTitleText;
        public string ItemsTitleText
        {
            get { return itemsTitleText; }
            set
            {
                itemsTitleText = value;
                NotifyPropertyChanged();
            }
        }

        string toPayText;
        public string ToPayText
        {
            get { return toPayText; }
            set
            {
                toPayText = value;
                NotifyPropertyChanged();
            }
        }
        
        private ObservableCollection<ItemViewModel> itemsList;
        public ObservableCollection<ItemViewModel> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                NotifyPropertyChanged();
            }
        }
    }
}
