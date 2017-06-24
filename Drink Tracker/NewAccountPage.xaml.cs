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
