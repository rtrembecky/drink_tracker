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
        public DrinksPageViewModel(string type)
        {
            HeaderDesc = "Pick a " + type + " to add to bill";

            DatabaseManager manager = new DatabaseManager();
            List<Drink> drinkList = manager.GetDrinksByType(type);
            drinks = new ObservableCollection<DrinkViewModel>(drinkList.Select(d => new DrinkViewModel(d)));
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
