﻿using Drink_Tracker.Model;
using Drink_Tracker.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Drink_Tracker
{
    public sealed partial class EditAccountPage : Page
    {
        public EditAccountPage()
        {
            this.InitializeComponent();
        }

        Account account;
        EditAccountPageViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Account)e.Parameter;

            viewModel = new EditAccountPageViewModel(account);
            this.DataContext = viewModel;
            
            if (account.Man) Man.IsChecked = true;
                        else Woman.IsChecked = true;

            base.OnNavigatedTo(e);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
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
                if (aWeight < 20 || aWeight > 500)
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
                DatabaseManager manager = new DatabaseManager();

                foreach (Account existingAcc in manager.GetAccounts())
                {
                    if (aUsername == existingAcc.Username && account.AccountId != existingAcc.AccountId)
                    {
                        ExistenceText.Visibility = Visibility.Visible;
                        break;
                    }
                    ExistenceText.Visibility = Visibility.Collapsed;
                };
                               
                if (ExistenceText.Visibility == Visibility.Collapsed)
                {
                    foreach (Account acc in manager.GetAccounts())
                    {
                        if (acc.AccountId == account.AccountId)
                        {
                            acc.Username = Username.Text;
                            acc.Man = Man.IsChecked.Value;
                            acc.WeightInKg = int.Parse(Weight.Text);
                            manager.UpdateAccount(acc);
                            break;
                        }
                    };

                }
                this.Frame.Navigate(typeof(AccountsPage));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AccountsPage));
        }
    }
}
