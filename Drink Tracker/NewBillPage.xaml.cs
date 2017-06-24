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
    public sealed partial class NewBillPage : Page
    {
        public NewBillPage()
        {
            this.InitializeComponent();
        }

        Account account;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Account)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var bill = new Bill
            {
                Created = DateTime.Now,
                Name = Bill_name.Text
            };
            account.Bills = new List<Bill>();
            account.Bills.Add(bill);
            DatabaseManager manager = new DatabaseManager();
            manager.UpdateAccount(account);
            this.Frame.Navigate(typeof(BillsPage), account);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BillsPage), account);
        }
    }
}
