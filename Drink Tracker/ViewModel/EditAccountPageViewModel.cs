using Drink_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class EditAccountPageViewModel : ViewModelBase
    {
        public EditAccountPageViewModel(Account account)
        {
            header = "Editing " + account.Username;
            username = account.Username;
            weight = account.WeightInKg.ToString();
        }

        string header;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                NotifyPropertyChanged();
            }
        }

        string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged();
            }
        }

        string weight;
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                NotifyPropertyChanged();
            }
        }
    }
}
