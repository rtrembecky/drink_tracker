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
    public sealed partial class DrinkTypesPage : Page
    {
        public DrinkTypesPage()
        {
            this.InitializeComponent();
        }

        private void DrinkTypesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.ClickedItem);
        }

        private void ButtonBeer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }

        private void ButtonWine_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }

        private void ButtonShots_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }

        private void ButtonNonalco_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }

        private void ButtonCocktails_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }

        private void ButtonOther_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), e.OriginalSource);
        }
    }
}
