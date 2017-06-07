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
        public AccountsPage()
        {
            this.InitializeComponent();
        }

        private void AccountsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(BillsPage), e.ClickedItem);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountContext())
            {
                AccountsList.ItemsSource = db.Accounts.ToList();
                foreach (var acc in AccountsList.Items)
                {
                    // perhaps some pic could be added in the future
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewAccountPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var db = new AccountContext())
            {
                AccountsList.ItemsSource = db.Accounts.ToList();
            }
            base.OnNavigatedTo(e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog(sender);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var acc = (sender as FrameworkElement).DataContext as Account;
            this.Frame.Navigate(typeof(EditAccountPage), acc);
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
                using (var db = new AccountContext())
                {
                    var acc = (sender as FrameworkElement).DataContext as Account;
                    db.Accounts.Remove(acc);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(AccountsPage));
            }
        }
    }
}
