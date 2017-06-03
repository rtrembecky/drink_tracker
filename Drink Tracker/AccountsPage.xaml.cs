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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Drink_Tracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                    //som myslel, ze nejak pridam obrazok... asi ne
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

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void DeleteDialog()
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete account?",
                Content = "If you delete this account, you won't be able to recover it. Are you sure you want to delete it?",
                SecondaryButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
        }
    }
}
