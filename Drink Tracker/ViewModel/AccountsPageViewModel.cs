using Drink_Tracker.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Drink_Tracker.ViewModel
{
    public class AccountsPageViewModel : ViewModelBase
    {
        public AccountsPageViewModel()
        {
            DatabaseManager manager = new DatabaseManager();
            List<Account> accountList = manager.GetAccounts();
            Accounts = new ObservableCollection<AccountViewModel>(accountList.Select(a => new AccountViewModel(a)));
        }

        private ObservableCollection<AccountViewModel> accounts;
        public ObservableCollection<AccountViewModel> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                NotifyPropertyChanged();
            }
        }
    }
}
