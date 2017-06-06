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

            HeaderText.Text = "New custom " + billAndType.Type;
            
            switch (billAndType.Type)
            {
                case "Beer":
                    ABV.Text = "4";
                    Volume.Text = "5";
                    Cost.Text = "30";
                    break;
                case "Wine":
                    ABV.Text = "12";
                    Volume.Text = "2";
                    Cost.Text = "40";
                    break;
                case "Shot":
                    ABV.Text = "38";
                    Volume.Text = "0.4";
                    Cost.Text = "40";
                    break;
                case "Nonalco":
                    ABV.Text = "0";
                    Volume.Text = "5";
                    Cost.Text = "30";
                    break;
                case "Cocktail":
                    ABV.Text = "15";
                    Volume.Text = "1.8";
                    Cost.Text = "80";
                    break;
                case "Other":
                    ABV.Text = "0";
                    Volume.Text = "0";
                    Cost.Text = "20";
                    break;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountContext())
            {
                bool viable = true;

                String dName = DrinkName.Text;
                if (dName.Length > 30)
                {
                    TooLongText.Visibility = Visibility.Visible;
                    EmptyText.Visibility = Visibility.Collapsed;
                    viable = false;
                }
                else
                {
                    TooLongText.Visibility = Visibility.Collapsed;
                    if (dName.Length == 0)
                    {
                        EmptyText.Visibility = Visibility.Visible;
                        viable = false;
                    }
                    else
                    {
                        EmptyText.Visibility = Visibility.Collapsed;
                    }
                }
                
                float foo = (float)0;
                float dABV = (float)0;
                if (!float.TryParse(ABV.Text, out foo))
                {
                    NotNumberABVText.Visibility = Visibility.Visible;
                    NotValidABVText.Visibility = Visibility.Collapsed;
                    viable = false;
                }
                else
                {
                    NotNumberABVText.Visibility = Visibility.Collapsed;
                    dABV = float.Parse(ABV.Text);
                    if (dABV < 0 || dABV > 100)
                    {
                        NotValidABVText.Visibility = Visibility.Visible;
                        viable = false;
                    }
                    else
                    {
                        NotValidABVText.Visibility = Visibility.Collapsed;
                    }
                }

                int dVolume = 0;
                if (!float.TryParse(Volume.Text, out foo))
                {
                    NotNumberVolumeText.Visibility = Visibility.Visible;
                    NotValidVolumeText.Visibility = Visibility.Collapsed;
                    viable = false;
                }
                else
                {
                    NotNumberVolumeText.Visibility = Visibility.Collapsed;
                    dVolume = (int)(float.Parse(Volume.Text) * 100);
                    if (dVolume < 0 || dVolume > 25000)
                    {
                        NotValidVolumeText.Visibility = Visibility.Visible;
                        viable = false;
                    }
                    else
                    {
                        NotValidVolumeText.Visibility = Visibility.Collapsed;
                    }
                }

                String dType = billAndType.Type;

                if (viable)
                {
                    var drink = new Drink
                    {
                        Name = dName,
                        ABV = dABV,
                        Prices = new List<Price>(),
                        Type = dType,
                        VolumeInMl = dVolume
                    };

                    foreach (Drink existingdrink in db.Drinks)
                    {
                        if (dName == existingdrink.Name && dABV == existingdrink.ABV && dType == existingdrink.Type && dVolume == existingdrink.VolumeInMl)
                        {
                            ExistenceText.Visibility = Visibility.Visible;
                            break;
                        }
                        ExistenceText.Visibility = Visibility.Collapsed;
                    };

                    Price price = new Price() { Value = float.Parse(Cost.Text) };
                    //TODO: ak price uz je v db, tak
                    // price = najdeny price
                    // inak ho tam pridaj
                    price.Drink = drink;
                
                    //price = db.Prices.Where(p => p.Value == price.Value).ToList().First();

                    drink.Prices.Add(price);

                    db.Prices.Add(price);
                    if (ExistenceText.Visibility == Visibility.Collapsed)
                    {
                        db.Drinks.Add(drink);
                        db.SaveChanges();
                        this.Frame.Navigate(typeof(DrinksPage), billAndType);
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //TODO set as proper back instead of redirect
            this.Frame.Navigate(typeof(DrinksPage), billAndType);
        }
    }
}
