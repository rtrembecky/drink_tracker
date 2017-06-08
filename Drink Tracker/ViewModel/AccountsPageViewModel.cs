using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class AccountsPageViewModel : ViewModelBase
    {
        public AccountsPageViewModel()
        {
            using (var db = new AccountContext())
            {
                Accounts = new ObservableCollection<AccountViewModel>(db.Accounts.ToList().Select(a => new AccountViewModel(a)));
            }
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
