using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class DatabaseManager : ViewModelBase
    {
        private Account account;

        public ObservableCollection<BillViewModel> ShowDrinks(Account a)
        {
            account = a;

            using (var db = new AccountContext())
            {
                List<Bill> billList = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();

                foreach (var bill in billList)
                {
                    bill.Items = db.Items
                        .Where(i => i.BillId == bill.BillId)
                        .ToList();

                    foreach (var item in bill.Items)
                    {
                        item.Drink = db.Drinks
                            .Find(item.DrinkId);

                        item.Ytems = db.Ytems
                            .Where(y => y.ItemId == item.ItemId)
                            .ToList();
                    }
                }

                return new ObservableCollection<BillViewModel>(billList.Select(b => new BillViewModel(b)).OrderByDescending(b => b.Created));
            }
        }
        
        private Bill bill;
        private string type;
        List<Drink> drinksByType;

        public ObservableCollection<DrinkViewModel> ShowDrinks(BillAndType b)
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

                return new ObservableCollection<DrinkViewModel>(drinksByType.Select(d => new DrinkViewModel(d)));
            }
        }
    }
}
