using Drink_Tracker.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Drink_Tracker
{
    public sealed partial class NewAccountPage : Page
    {
        public NewAccountPage()
        {
            this.InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ExistenceText.Visibility = Visibility.Collapsed;

            bool viable = true;

            String aUsername = Username.Text;
            if (aUsername.Length > 30)
            {
                TooLongText.Visibility = Visibility.Visible;
                EmptyText.Visibility = Visibility.Collapsed;
                viable = false;
            }
            else
            {
                TooLongText.Visibility = Visibility.Collapsed;
                if (aUsername.Length == 0)
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
            int aWeight = 0;
            if (!float.TryParse(Weight.Text, out foo))
            {
                NotNumberWeightText.Visibility = Visibility.Visible;
                NotValidWeightText.Visibility = Visibility.Collapsed;
                viable = false;
            }
            else
            {
                NotNumberWeightText.Visibility = Visibility.Collapsed;
                aWeight = (int)(float.Parse(Weight.Text));
                if (aWeight <= 20 || aWeight > 500)
                {
                    NotValidWeightText.Visibility = Visibility.Visible;
                    viable = false;
                }
                else
                {
                    NotValidWeightText.Visibility = Visibility.Collapsed;
                }
            }

            if (viable)
            {
                var account = new Account
                {
                    Username = aUsername,
                    Man = Man.IsChecked.Value,
                    WeightInKg = aWeight
                };

                DatabaseManager manager = new DatabaseManager();

                foreach (Account existingAcc in manager.GetAccounts())
                {
                    if (aUsername == existingAcc.Username)
                    {
                        ExistenceText.Visibility = Visibility.Visible;
                        break;
                    }
                    ExistenceText.Visibility = Visibility.Collapsed;
                };

                if (ExistenceText.Visibility == Visibility.Collapsed)
                {
                    account.Username = Username.Text;
                    account.Man = Man.IsChecked.Value;
                    account.WeightInKg = int.Parse(Weight.Text);
                    manager.CreateAccount(account);
                    this.Frame.Navigate(typeof(AccountsPage));
                }

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AccountsPage));
        }
    }
}
