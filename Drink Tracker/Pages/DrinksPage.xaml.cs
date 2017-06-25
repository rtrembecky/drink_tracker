using Drink_Tracker.Model;
using Drink_Tracker.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class DrinksPage : Page
    {
        public DrinksPage()
        {
            this.InitializeComponent();
        }
        
        BillAndType billAndType;
        DrinksPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            billAndType = (BillAndType)e.Parameter;

            viewModel = new DrinksPageViewModel(billAndType.Type);
            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        private void DrinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DrinkViewModel drinkvm = (DrinkViewModel)e.ClickedItem;
            Drink drink = drinkvm.Drink;

            //this.Frame.Navigate(typeof(DrinksPage), (e.ClickedItem as DrinkViewModel).Drink);

            //sprav buydrink
            DatabaseManager manager = new DatabaseManager();
            manager.BuyDrink(drink, billAndType.Bill);
            //

            this.Frame.Navigate(typeof(ItemsPage), billAndType.Bill);
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
                var drink = (sender as FrameworkElement).DataContext as DrinkViewModel;
                DatabaseManager manager = new DatabaseManager();
                manager.RemoveDrink(drink.Drink);
                this.Frame.Navigate(typeof(DrinksPage), billAndType);
            }
        }
    }
}
