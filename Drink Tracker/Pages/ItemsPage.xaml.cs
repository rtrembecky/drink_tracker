using Drink_Tracker.Model;
using Drink_Tracker.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class ItemsPage : Page
    {
        public ItemsPage()
        {
            this.InitializeComponent();
        }

        Bill currentbill;
        ItemsPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().BackRequested += BackToBills;

            currentbill = (Bill)e.Parameter;

            viewModel = new ItemsPageViewModel(currentbill);
            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().BackRequested -= BackToBills;
            base.OnNavigatedFrom(e);
        }

        private void ItemsListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
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

        //private void BackToBills(object s, BackRequestedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(BillsPage), account);
        //}

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
                var timestampView = (sender as FrameworkElement).DataContext as TimestampViewModel;
                Timestamp timestamp = timestampView.Timestamp;
                var item = currentbill.Items.Find(i => i.ItemId == timestamp.ItemId);

                DatabaseManager manager = new DatabaseManager();
                if (item.Timestamps.Count == 1)
                    manager.RemoveItem(item);
                else
                    manager.RemoveTimestamp(timestamp);
                
                this.Frame.Navigate(typeof(ItemsPage), currentbill);
            }
        }

        private void Collapse_Toggle(object sender, ItemClickEventArgs e)
        {
            var itemView = e.ClickedItem as ItemViewModel;
            itemView.Expanded = !itemView.Expanded;
        }
    }
}
