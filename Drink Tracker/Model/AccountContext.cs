using Microsoft.EntityFrameworkCore;

namespace Drink_Tracker.Model
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Timestamp> Timestamps { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=d.db");
        }
    }
}
