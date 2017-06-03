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
    public sealed partial class BillsPage : Page
    {
        public BillsPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                this.Frame.Navigate(typeof(AccountsPage));
            };
        }

        Account account;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Account)e.Parameter;

            using (var db = new AccountContext())
            {
                var bills = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();

                BillsList.ItemsSource = bills;
            }

            BillsHeaderTitle.Text = account.Username;
            CalculationTitle.Text = "Around XX.XX‰. Estimated 0‰ at YY:YY.";
            
            base.OnNavigatedTo(e);
        }

        // nebude viest na drinktypes ale na konkretny ucet z vecera
        private void BillsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(YtemsPage), e.ClickedItem);
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
    }
}
