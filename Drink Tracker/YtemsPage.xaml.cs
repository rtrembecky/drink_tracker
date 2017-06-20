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
    public sealed partial class YtemsPage : Page
    {
        public YtemsPage()
        {
            this.InitializeComponent();
        }

        Bill currentbill;
        Account account;
        float TotalPrice = 0;
        YtemsPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackToBills;

            viewModel = new YtemsPageViewModel((Bill)e.Parameter);
            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
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
            DeleteDialog(sender);
        }

        private async void DeleteDialog(object sender)
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete drink?",
                Content = "Are you sure you want to delete this drink from the bill?",
                SecondaryButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                using (var db = new AccountContext())
                {
                    var ytem = (sender as FrameworkElement).DataContext as Ytem;
                    var item = currentbill.Items.Find(i => i.ItemId == ytem.ItemId);
                    if (item.Ytems.Count == 1)
                        db.Items.Remove(item);
                    else
                        db.Ytems.Remove(ytem);
                    db.SaveChanges();
                }
                this.Frame.Navigate(typeof(YtemsPage), currentbill);
            }
        }

        private void Collapse_Toggle(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as Item;
            item.Expanded = !item.Expanded;
        }
    }
}
