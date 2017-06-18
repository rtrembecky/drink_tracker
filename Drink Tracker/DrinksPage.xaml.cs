using Drink_Tracker.ViewModel;
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
        //List<Drink> drinksByType;
        DrinksPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = new DrinksPageViewModel((BillAndType)e.Parameter);
            this.DataContext = viewModel;

            billAndType = (BillAndType)e.Parameter;
            bill = billAndType.Bill;
            type = billAndType.Type;

            base.OnNavigatedTo(e);
        }

        private void DrinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Drink drink = (Drink)e.ClickedItem;

            //sprav buydrink
            this.Frame.Navigate(typeof(DrinksPage), (e.ClickedItem as DrinkViewModel).Drink);
            //

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
                    var drink = (sender as FrameworkElement).DataContext as DrinkViewModel;
                    db.Drinks.Remove(drink.Drink);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(DrinksPage), billAndType);
            }
        }
    }
}
