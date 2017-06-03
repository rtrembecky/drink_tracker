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
    public sealed partial class NewDrinkPage : Page
    {
        public NewDrinkPage()
        {
            this.InitializeComponent();
        }

        BillAndType billAndType;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            billAndType = (BillAndType)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountContext())
            {
                var drink = new Drink
                {
                    Name = DrinkName.Text,
                    ABV = float.Parse(ABV.Text),
                    PriceInKc = int.Parse(Cost.Text),
                    Type = billAndType.type,
                    VolumeInMl = int.Parse(Volume.Text)
                };
                db.Drinks.Add(drink);
                db.SaveChanges();
            }
            this.Frame.Navigate(typeof(DrinksPage), billAndType);
        }
    }
}
