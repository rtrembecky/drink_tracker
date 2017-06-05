﻿using System;
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
using Drink_Tracker.Converters;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Drink_Tracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrinksPage : Page
    {
        public DrinksPage()
        {
            this.InitializeComponent();
        }

        string type;
        Bill bill;
        BillAndType billAndType;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            billAndType = (BillAndType)e.Parameter;
            type = billAndType.type;
            bill = billAndType.bill;

            using (var db = new AccountContext())
            {
                var drinksByType = db.Drinks
                    .Where(drink => drink.Type == type)
                    .ToList();

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
                //TODO: osetrit, ci item uz je na liste a nejak zgrupit? premysliet
                bill.Items = db.Items
                    .Where(i => i.BillId == bill.BillId)
                    .ToList();
                var item = new Item
                {
                    Added = DateTime.Now,
                    BillId = bill.BillId,
                    DrinkId = drink.DrinkId
                };
                //bill.Items.Add(item);
                db.Items.Add(item);
                //db.Bills.Update(bill);
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Drink drink = (sender as FrameworkElement).DataContext as Drink;
            DrinkBillType dab = new DrinkBillType(drink, billAndType);
            this.Frame.Navigate(typeof(EditDrinkPage), dab);
        }

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
