using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Windows.UI.Xaml.Media;

namespace Drink_Tracker
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=accounts.db");
        }
    }

    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public int WeightInKg { get; set; }
        public bool Man { get; set; }
        public List<Bill> Bills { get; set; }
    }

    public class Bill
    {
        public int BillId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public List<Item> Items { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public DateTime Added { get; set; }
        public string ImageSource { get; set; }
        public Drink Drink { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }

    public class Drink
    {
        public int DrinkId { get; set; }
        public float ABV { get; set; }
        public string Name { get; set; }
        public float PriceInKc { get; set; }
        public string Type { get; set; }
        public int VolumeInMl { get; set; }
    }

    public class BillAndType
    {
        public Bill bill { get; set; }
        public string type { get; set; }
    }
}
