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
using Drink_Tracker.Converters;

namespace Drink_Tracker
{
    public sealed partial class EditDrinkPage : Page
    {
        public EditDrinkPage()
        {
            this.InitializeComponent();
        }

        Drink drink;
        BillAndType billAndType;
        DrinkBillType dab;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dab = (DrinkBillType)e.Parameter;
            drink = dab.Drink;
            billAndType = dab.BillAndType;
            base.OnNavigatedTo(e);

            HeaderText.Text = "Editing " + drink.Name;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountContext())
            {
                db.Drinks.Update(drink);
                db.SaveChanges();
            }
            this.Frame.Navigate(typeof(DrinksPage), billAndType);
        }
    }
}
