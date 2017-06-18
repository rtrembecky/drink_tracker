using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    class DrinksPageViewModel : ViewModelBase
    {
        private Bill bill;
        private string type;
        List<Drink> drinksByType;

        public DrinksPageViewModel(BillAndType b)
        {
            bill = b.Bill;
            type = b.Type;

            using (var db = new AccountContext())
            {
                drinksByType = db.Drinks
                    .Where(drink => drink.Type == type)
                    .ToList();

                foreach (var d in drinksByType)
                {
                    d.Prices = db.Prices
                        .Where(p => p.DrinkId == d.DrinkId)
                        .ToList();
                }
                
                //drinksList.ItemsSource = drinksByType;

                HeaderDesc = "Pick a " + type + " to add to bill";

                drinks = new ObservableCollection<DrinkViewModel>(drinksByType.Select(d => new DrinkViewModel(d)));
            }
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
