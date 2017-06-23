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
    public sealed partial class AccountsPage : Page
    {
        AccountsPageViewModel viewModel;

        public AccountsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = new AccountsPageViewModel();
            this.DataContext = viewModel;
            base.OnNavigatedTo(e);
        }

        private void AccountsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(BillsPage), (e.ClickedItem as AccountViewModel).Account);
        }
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewAccountPage));
        }
        
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog(sender);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var acc = (sender as FrameworkElement).DataContext as AccountViewModel;
            this.Frame.Navigate(typeof(EditAccountPage), acc.Account);
        }

        private async void DeleteDialog(object sender)
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete account?",
                Content = "If you delete this account, you won't be able to recover it. Are you sure you want to delete it?",
                SecondaryButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var acc = (sender as FrameworkElement).DataContext as AccountViewModel;
                DatabaseManager manager = new DatabaseManager();
                manager.RemoveAccount(acc.Account);
                
                this.Frame.Navigate(typeof(AccountsPage));
            }
        }
    }
}
