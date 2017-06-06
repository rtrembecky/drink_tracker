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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Drink_Tracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YtemsPage : Page
    {
        public YtemsPage()
        {
            this.InitializeComponent();
        }

        Bill currentbill;
        Account account;
        List<Item> items;
        float TotalPrice = 0;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackToBills;

            currentbill = (Bill)e.Parameter;

            using (var db = new AccountContext())
            {
                items = db.Items
                    .Where(item => item.BillId == currentbill.BillId)
                    .ToList();

                foreach (var item in items)
                {
                    item.Drink = db.Drinks
                        .Where(drink => item.DrinkId == drink.DrinkId)
                        .ToList()
                        .First();

                    item.Drink.Prices = db.Prices
                        .Where(p => p.DrinkId == item.Drink.DrinkId)
                        .ToList();

                    item.Ytems = db.Ytems
                        .Where(y => y.ItemId == item.ItemId)
                        .OrderByDescending(y => y.Added)
                        .ToList();
                }

                YtemsList.ItemsSource = items;

                account = db.Accounts
                    .Where(a => a.AccountId == currentbill.AccountId)
                    .ToList()
                    .First();
            }

            YtemsHeaderTitle.Text = currentbill.Name + " bill";

            Calculation();

            ToPay.Text = "To pay: " + TotalPrice.ToString("0.00") + " CZK";

            base.OnNavigatedTo(e);
        }

        private void Calculation()
        {
            TotalPrice = 0;
            foreach (var i in items)
            {
                TotalPrice += i.DrinkPrice * i.Ytems.Count;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackToBills;
            base.OnNavigatedFrom(e);
        }

        private void YtemsListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
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
            this.Frame.Navigate(typeof(DrinkTypesPage), currentbill);
        }

        private void BackToBills(object s, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(BillsPage), account);
        }

        private void Flag_Toggle(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Collapse_Toggle(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as Item;
            item.Expanded = !item.Expanded;
        }
    }
}
