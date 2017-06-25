using Drink_Tracker.Model;
using Drink_Tracker.ViewModel;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class NewDrinkPage : Page
    {
        public NewDrinkPage()
        {
            this.InitializeComponent();
        }

        BillAndType billAndType;
        NewDrinkPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            billAndType = (BillAndType)e.Parameter;

            viewModel = new NewDrinkPageViewModel(billAndType);
            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ExistenceText.Visibility = Visibility.Collapsed;

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

            DatabaseManager manager = new DatabaseManager();

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

                foreach (Drink existingdrink in manager.GetDrinks())
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
                
                if (ExistenceText.Visibility == Visibility.Collapsed)
                {
                    manager.CreateDrink(drink);
                    this.Frame.Navigate(typeof(DrinksPage), billAndType);
                }
                else
                    manager.CreatePrice(price);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrinksPage), billAndType);
        }
    }
}
