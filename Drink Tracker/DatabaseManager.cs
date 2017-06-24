using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker
{
    public class DatabaseManager
    {
        public List<Account> GetAccounts()
        {
            using (var db = new AccountContext())
            {
                return db.Accounts.ToList();
            }
        }

        public List<Bill> GetBillsByAccount(Account account)
        {
            using (var db = new AccountContext())
            {
                account.Bills = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();
            }

            return account.Bills;
        }

        public List<Bill> GetBillsFullByAccount(Account account)
        {
            using (var db = new AccountContext())
            {
                account.Bills = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();

                foreach (var bill in account.Bills)
                {
                    bill.Items = GetItemsFullByBill(bill);
                }
            }

            return account.Bills;
        }

        public List<Item> GetItemsByBill(Bill bill)
        {
            using (var db = new AccountContext())
            {
                bill.Items = db.Items
                    .Where(item => item.BillId == bill.BillId)
                    .ToList();
            }

            return bill.Items;
        }

        public List<Item> GetItemsFullByBill(Bill bill)
        {
            using (var db = new AccountContext())
            {
                bill.Items = db.Items
                    .Where(item => item.BillId == bill.BillId)
                    .ToList();

                foreach (var item in bill.Items)
                {
                    item.Drink = db.Drinks
                        .Find(item.DrinkId);

                    item.Drink.Prices = db.Prices
                        .Where(p => p.DrinkId == item.Drink.DrinkId)
                        .ToList();

                    item.Timestamps = GetTimestampsByItem(item);
                }
            }

            return bill.Items;
        }

        public List<Timestamp> GetTimestampsByItem(Item item)
        {
            using (var db = new AccountContext())
            {
                item.Timestamps = db.Timestamps
                    .Where(y => y.ItemId == item.ItemId)
                    .OrderByDescending(y => y.Added)
                    .ToList();

                foreach (var timestamp in item.Timestamps)
                {
                    timestamp.Item = item;
                }
            }

            return item.Timestamps;
        }

        public List<Drink> GetDrinksByType(string type)
        {
            List<Drink> drinks;

            using (var db = new AccountContext())
            {
                drinks = db.Drinks
                    .Where(drink => drink.Type == type)
                    .ToList();

                foreach (var d in drinks)
                {
                    d.Prices = db.Prices
                        .Where(p => p.DrinkId == d.DrinkId)
                        .ToList();
                }
            }

            return drinks;
        }

        public void BuyDrink(Drink d, Bill bill)
        {
            using (var db = new AccountContext())
            {
                bill.Items = GetItemsFullByBill(bill);

                var item = bill.Items.Find(it => it.DrinkId == d.DrinkId && it.DrinkPrice == d.Prices.First().Value);
                if (item == null)
                {
                    item = new Item
                    {
                        Timestamps = new List<Timestamp>(),
                        BillId = bill.BillId,
                        DrinkId = d.DrinkId,
                        DrinkPrice = d.Prices.First().Value
                    };
                    item.Timestamps.Add(new Timestamp { Added = DateTime.Now });
                    db.Items.Add(item);
                }
                else
                {
                    item.Timestamps.Add(new Timestamp { Added = DateTime.Now });
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
                a.Bills = new List<Bill>();
                a.Bills.Add(bill);
                db.Accounts.Update(a);
                db.SaveChanges();
            }
        }

        public void UpdateAccount(Account account)
        {
            using (var db = new AccountContext())
            {
                db.Accounts.Update(account);
                db.SaveChanges();
            }
        }
        
        public void RemoveAccount(Account account)
        {
            using (var db = new AccountContext())
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
        }

        public void RemoveBill(Bill bill)
        {
            using (var db = new AccountContext())
            {
                db.Bills.Remove(bill);
                db.SaveChanges();
            }
        }

        public void RemoveDrink(Drink drink)
        {
            using (var db = new AccountContext())
            {
                db.Drinks.Remove(drink);
                db.SaveChanges();
            }
        }

        public void RemoveItem(Item item)
        {
            using (var db = new AccountContext())
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
        }

        public void RemoveTimestamp(Timestamp timestamp)
        {
            using (var db = new AccountContext())
            {
                db.Timestamps.Remove(timestamp);
                db.SaveChanges();
            }
        }

        public void UpdateBill(Bill bill)
        {
            using (var db = new AccountContext())
            {
                db.Bills.Update(bill);
                db.SaveChanges();
            }
        }
    }
}
