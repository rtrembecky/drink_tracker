using Drink_Tracker.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class DrinkTypesPage : Page
    {
        public DrinkTypesPage()
        {
            this.InitializeComponent();
        }

        Bill currentBill;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentBill = (Bill)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void ButtonBeer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Beer" } );
        }

        private void ButtonWine_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Wine" } );
        }

        private void ButtonShots_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Shot" } );
        }

        private void ButtonNonalco_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Nonalco" } );
        }

        private void ButtonCocktails_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Cocktail" } );
        }

        private void ButtonOther_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), new BillAndType { Bill = currentBill, Type = "Other" } );
        }
    }
}
