using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class DrinksPageViewModel : ViewModelBase
    {
        private Bill bill;
        private string type;
        List<Drink> drinksByType;

        public DrinksPageViewModel(BillAndType b)
        {
            HeaderDesc = "Pick a " + b.Type + " to add to bill";

            drinks = new DatabaseManager.ShowDrinks(drinksByType);
        }

        string headerDesc;
        public string HeaderDesc
        {
            get { return headerDesc; }
            set
            {
                headerDesc = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<DrinkViewModel> drinks;
        public ObservableCollection<DrinkViewModel> Drinks
        {
            get { return drinks; }
            set
            {
                drinks = value;
                NotifyPropertyChanged();
            }
        }
    }
}
