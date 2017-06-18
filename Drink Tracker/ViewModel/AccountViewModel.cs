﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Drink_Tracker.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        Account account;

        public AccountViewModel(Account a)
        {
            account = a;
            // Sex = a.Man ? man : woman;
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

        private ImageSource sex;
        public ImageSource Sex
        {
            get { return sex; }
            set
            {
                sex = value;
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