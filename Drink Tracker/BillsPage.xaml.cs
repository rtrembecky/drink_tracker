using Drink_Tracker.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class BillsPage : Page
    {
        public BillsPage()
        {
            this.InitializeComponent();
        }

        BillsPageViewModel viewModel;

        Account account;
        BillViewModel editedBill;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackToAccount;

            viewModel = new BillsPageViewModel((Account)e.Parameter);
            this.DataContext = viewModel;
            
            if (account.Man)
                BillsHeaderStats.Text = "Male, " + account.WeightInKg + " kg";
            else
                BillsHeaderStats.Text = "Female, " + account.WeightInKg + " kg";

            account = (Account)e.Parameter;
            
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackToAccount;
            base.OnNavigatedFrom(e);
        }

        private void BillsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(YtemsPage), (e.ClickedItem as BillViewModel).Bill);
        }

        private void BillsListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.ItemIndex % 2 == 0)
            {
                args.ItemContainer.Background = new SolidColorBrush(Windows.UI.Colors.LightYellow);
            }
            else
            {
                args.ItemContainer.Background = new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewBillPage), account);
        }

        private void BackToAccount(object s, BackRequestedEventArgs e)
        {
           this.Frame.Navigate(typeof(AccountsPage), account);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editedBill = (sender as FrameworkElement).DataContext as BillViewModel;
            editedBill.Edited = true;
        }

        private void Confirm_Edit_Click(object sender, RoutedEventArgs e)
        {
            editedBill.Edited = false;
            editedBill.Name = editedBill.EditField;

            using (var db = new AccountContext())
            {
                db.Bills.Update(editedBill.Bill);
                db.SaveChanges();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog(sender);
        }

        private async void DeleteDialog(object sender)
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete bill?",
                Content = "If you delete this bill, you won't be able to recover it. Are you sure you want to delete it?",
                SecondaryButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                using (var db = new AccountContext())
                {
                    var bill = (sender as FrameworkElement).DataContext as BillViewModel;
                    db.Bills.Remove(bill.Bill);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(BillsPage), account);
            }
        }

        private void Cancel_Edit_Click(object sender, RoutedEventArgs e)
        {
            editedBill = (sender as FrameworkElement).DataContext as BillViewModel;
            editedBill.Edited = false;
        }

        private void EditName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
