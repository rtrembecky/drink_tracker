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
    public sealed partial class EditAccountPage : Page
    {
        public EditAccountPage()
        {
            this.InitializeComponent();
        }

        Account account;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Account)e.Parameter;
            EditAccountHeader.Text = "Editing " + account.Username;
            Username.Text = account.Username;
            if (account.Man) Man.IsChecked = true;
                        else Woman.IsChecked = true;
            Weight.Text = account.WeightInKg.ToString();

            base.OnNavigatedTo(e);
        }

        private void Edit_click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountContext())
            {
                foreach (Account acc in db.Accounts)
                {
                    if (acc.AccountId == account.AccountId)
                    {
                        acc.Username = Username.Text;
                        acc.Man = Man.IsChecked.Value;
                        acc.WeightInKg = int.Parse(Weight.Text);
                        break;
                    }
                };
                db.SaveChanges();
            }
            this.Frame.Navigate(typeof(AccountsPage));
        }
    }
}
