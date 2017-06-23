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

        public ObservableCollection<BillViewModel> GetBills(Account a)
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

        public ObservableCollection<DrinkViewModel> GetDrinks(BillAndType b)
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

                return new ObservableCollection<DrinkViewModel>(drinksByType.Select(d => new DrinkViewModel(d)));
            }
        }

        public void BuyDrink(Drink d, Bill bill)
        {
            using (var db = new AccountContext())
            {
                bill.Items = db.Items
                    .Where(it => it.BillId == bill.BillId)
                    .ToList();

                foreach (var i in bill.Items)
                {
                    i.Ytems = db.Ytems
                        .Where(y => y.ItemId == i.ItemId)
                        .ToList();
                }

                var item = bill.Items.Find(it => it.DrinkId == d.DrinkId && it.DrinkPrice == d.Prices.First().Value);
                if (item == null)
                {
                    item = new Item
                    {
                        Ytems = new List<Ytem>(),
                        BillId = bill.BillId,
                        DrinkId = d.DrinkId,
                        DrinkPrice = d.Prices.First().Value
                    };
                    item.Ytems.Add(new Ytem { Added = DateTime.Now });
                    db.Items.Add(item);
                }
                else
                {
                    item.Ytems.Add(new Ytem { Added = DateTime.Now });
                    db.Items.Update(item);
                }
                
                db.SaveChanges();
            }
        }

        public void CreateBill(Account a, String billname)
        {
            using (var db = new AccountContext())
            {
                var bill = new Bill
                {
                    Created = DateTime.Now,
                    Name = billname
                };
                account.Bills = new List<Bill>();
                account.Bills.Add(bill);
                db.Accounts.Update(account);
                db.SaveChanges();
            }
        }


        Bill currentbill;

        public ObservableCollection<ItemViewModel> GetYtems(Bill b)
        {
            currentbill = b;

            using (var db = new AccountContext())
            {
                currentbill.Items = db.Items
                    .Where(item => item.BillId == currentbill.BillId)
                    .ToList();

                foreach (var item in currentbill.Items)
                {
                    item.Drink = db.Drinks
                        .Where(drink => item.DrinkId == drink.DrinkId)
                        .ToList()
                        .First();

                    item.Drink.Prices = db.Prices
                        .Where(p => p.DrinkId == item.Drink.DrinkId)
                        .ToList();

                    item.Ytems = db.Ytems
                        .Where(y => y.ItemId == item.ItemId)
                        .OrderByDescending(y => y.Added)
                        .ToList();
                }

                account = db.Accounts
                    .Where(a => a.AccountId == currentbill.AccountId)
                    .ToList()
                    .First();
            }

            return new ObservableCollection<ItemViewModel>(currentbill.Items.Select(item => new ItemViewModel(item)));

        }
    }
}
