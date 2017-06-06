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
        }

        Account account;
        List<Bill> bills;
        float promille;
        DateTime sober;
        Bill editedbill;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackToAccount;

            account = (Account)e.Parameter;

            using (var db = new AccountContext())
            {
                bills = db.Bills
                    .Where(bill => bill.AccountId == account.AccountId)
                    .ToList();

                foreach (var bill in bills)
                {
                    bill.Items = db.Items
                        .Where(i => i.BillId == bill.BillId)
                        .ToList();

                    foreach (var item in bill.Items)
                    {
                        item.Drink = db.Drinks
                            .Where(d => d.DrinkId == item.DrinkId)
                            .ToList()
                            .First();

                        item.Ytems = db.Ytems
                            .Where(y => y.ItemId == item.ItemId)
                            .ToList();
                    }

                    bill.Edited = false;
                }

                BillsList.ItemsSource = bills;
            }

            BillsHeaderTitle.Text = account.Username;

            Calculation();
            
            if (promille == 0)
                CalculationTitle.Text = "You should be sober.";
            else
                CalculationTitle.Text = "Around " + Math.Round((decimal)promille, 3) + "‰. Sober at " + sober.ToString("HH:mm:ss") + ", " + sober.ToString("dd.MM") + ".";

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackToAccount;
            base.OnNavigatedFrom(e);
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
            float halfbac = (float)(10f / (account.WeightInKg * r));
            float alcograms = 0;
            DateTime t = DateTime.Now;
            TimeSpan elapsedTime = new TimeSpan(0, 0, 0);

            promille = 0;
            sober = DateTime.Now;
            if (bills != null && bills.Count != 0)
            {
                //TODO: fix this
                foreach (var bill in bills)
                {
                    if ((t.Subtract(bill.Created)).TotalDays >= 1)
                        break;
                    else
                    {
                        if (bill.Items != null && bill.Items.Count != 0)
                        {
                            foreach (var item in bill.Items)
                            {
                                foreach (var ytem in item.Ytems)
                                {
                                    alcograms = (float)(item.Drink.VolumeInMl * item.Drink.ABV * 0.01 * 0.789);
                                    elapsedTime = t.Subtract(ytem.Added);
                                    bac = (float)(halfbac * alcograms - (elapsedTime.TotalMinutes * 0.00025));
                                    if (bac > 0)
                                        totalbac = (float)(totalbac + bac);
                                }
                            }
                            promille = (float)(10 * totalbac);
                            sober = DateTime.Now.AddMinutes(totalbac / 0.00025);
                        }
                    }
                }
            }
        }

        private void BackToAccount(object s, BackRequestedEventArgs e)
        {
           this.Frame.Navigate(typeof(AccountsPage), account);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editedbill = (sender as FrameworkElement).DataContext as Bill;
            editedbill.Edited = true;
        }

        private void Confirm_Edit_Click(object sender, RoutedEventArgs e)
        {
            editedbill = (sender as FrameworkElement).DataContext as Bill;
            editedbill.Name = "Text field input";
            editedbill.Edited = false;
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
                    var bill = (sender as FrameworkElement).DataContext as Bill;
                    db.Bills.Remove(bill);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(BillsPage), account);
            }
        }

        private void Cancel_Edit_Click(object sender, RoutedEventArgs e)
        {
            editedbill = (sender as FrameworkElement).DataContext as Bill;
            editedbill.Edited = false;
        }
    }
}
