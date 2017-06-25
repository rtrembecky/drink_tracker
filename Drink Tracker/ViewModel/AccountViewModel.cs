using Drink_Tracker.Model;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;

namespace Drink_Tracker.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        Account account;

        public AccountViewModel(Account a)
        {
            account = a;
        }

        public Account Account
        {
            get { return account; }
        }

        public string Username
        {
            get { return account.Username; }
            set
            {
                account.Username = value;
                NotifyPropertyChanged();
            }
        }

        public int WeightInKg
        {
            get { return account.WeightInKg; }
            set
            {
                account.WeightInKg = value;
                NotifyPropertyChanged();
            }
        }

        public List<Bill> Bills
        {
            get { return account.Bills; }
            set
            {
                account.Bills = value;
                NotifyPropertyChanged();
            }
        }
    }
}
