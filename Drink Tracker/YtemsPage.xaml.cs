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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentbill = (Bill)e.Parameter;

            using (var db = new AccountContext())
            {
                var items = db.Items
                    .Where(item => item.BillId == currentbill.BillId)
                    .ToList();

                YtemsList.ItemsSource = items;
            }

            YtemsHeaderTitle.Text = currentbill.Name + " bill";

            base.OnNavigatedTo(e);
        }

        private void YtemsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinkTypesPage), e.ClickedItem);
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
        
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinkTypesPage), currentbill);
        }
    }
}
