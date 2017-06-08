using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class DrinksPage : Page
    {
        public DrinksPage()
        {
            this.InitializeComponent();
        }

        string type;
        Bill bill;
        BillAndType billAndType;
        List<Drink> drinksByType;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            billAndType = (BillAndType)e.Parameter;
            type = billAndType.Type;
            bill = billAndType.Bill;

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

                DrinksList.ItemsSource = drinksByType;
            }

            HeaderText.Text = "Pick a " + type + " to add to bill";

            base.OnNavigatedTo(e);
        }

        private void DrinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Drink drink = (Drink)e.ClickedItem;

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

                var item = bill.Items.Find(it => it.DrinkId == drink.DrinkId && it.DrinkPrice == drink.Prices.First().Value);
                if (item == null)
                {
                    item = new Item
                    {
                        Ytems = new List<Ytem>(),
                        BillId = bill.BillId,
                        DrinkId = drink.DrinkId,
                        DrinkPrice = drink.Prices.First().Value
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

            this.Frame.Navigate(typeof(YtemsPage), bill);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewDrinkPage), billAndType);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog(sender);
        }

        // if we decide editing drinks is a good idea
        //<AppBarButton Grid.Column="2" Icon="Edit" Click="Edit_Click"/>
        //private void Edit_Click(object sender, RoutedEventArgs e)
        //{ }

        private async void DeleteDialog(object sender)
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete drink?",
                Content = "If you delete this drink, you won't be able to recover it. Are you sure you want to delete it?",
                SecondaryButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                using (var db = new AccountContext())
                {
                    var drink = (sender as FrameworkElement).DataContext as Drink;
                    db.Drinks.Remove(drink);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(DrinksPage), billAndType);
            }
        }
    }
}
