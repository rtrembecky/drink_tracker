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
        List<Bill> bills;
        float promille;
        DateTime sober;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Account)e.Parameter;

            using (var db = new AccountContext())
            {
                bills = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();

                BillsList.ItemsSource = bills;
            }

            BillsHeaderTitle.Text = account.Username;

            Calculation();
            
            if (promille == 0)
                CalculationTitle.Text = "You should be sober.";
            else
                CalculationTitle.Text = "Around " + promille + "‰. Estimated 0‰ at " + sober + ".";

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

        private void Calculation()
        {
            int r;
            if (account.Man == true)
                r = 68;
            else
                r = 55;
            float bac = 0;
            float totalbac = 0;
            int halfbac = (10 / (account.WeightInKg * r));
            float alcograms = 0;
            DateTime t = DateTime.Now;
            TimeSpan elapsedTime = new TimeSpan(0, 0, 0);

            promille = 0;
            sober = DateTime.Now;
            if (bills != null && bills.Count != 0)
            {
                //TODO: expand to bills in 24 hours, not only last one
                //foreach bill in bills (ked je timespan teraz-created vacsi ako 48 tak breakni)
                Bill bill = bills.First<Bill>();
                if (bill.Items != null && bill.Items.Count != 0)
                {
                    foreach (var item in bill.Items)
                    {
                        alcograms = (float)(item.Drink.VolumeInMl * item.Drink.ABV * 0.789);
                        elapsedTime = t.Subtract(item.Added);
                        bac = (float)(halfbac * alcograms - (elapsedTime.TotalMinutes * 0.00025));
                        if (bac > 0)
                            totalbac = (float)(totalbac + bac);
                    }
                    promille = (float)(0.1 * totalbac);
                    sober = DateTime.Now.AddMinutes(totalbac / 0.00025);
                }
            }
        }
    }
}
