using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class BillsPageViewModel : ViewModelBase
    {
        public BillsPageViewModel(Account a)
        {
            account = a;

            DatabaseManager manager = new DatabaseManager();
            bills = manager.GetBills(account);

            if (account.Man)
                headerStats = "Male, " + account.WeightInKg + " kg";
            else
                headerStats = "Female, " + account.WeightInKg + " kg";

            Calculation();
        }

        private ObservableCollection<BillViewModel> bills;
        public ObservableCollection<BillViewModel> Bills
        {
            get { return bills; }
            set
            {
                bills = value;
                NotifyPropertyChanged();
            }
        }

        private Account account;

        public string Username
        {
            get { return account.Username; }
            set
            {
                account.Username = value;
                NotifyPropertyChanged();
            }
        }

        private string promilleText;
        public string PromilleText
        {
            get { return promilleText; }
            set
            {
                promilleText = value;
                NotifyPropertyChanged();
            }
        }

        string headerStats;
        public string HeaderStats
        {
            get { return headerStats; }
            set
            {
                headerStats = value;
                NotifyPropertyChanged();
            }
        }


        private void Calculation()
        {
            int r;
            if (account.Man == true)
                r = 68;
            else
                r = 55;
            float bac = 0;
            float totalbac = 0;
            float halfbac = (float)(10f / (account.WeightInKg * r));
            float alcograms = 0;
            DateTime t = DateTime.Now;
            TimeSpan elapsedTime = new TimeSpan(0, 0, 0);

            float promille = 0;
            DateTime sober = DateTime.Now;
            if (bills != null && bills.Count != 0)
            {
                foreach (var bill in bills)
                {
                    if ((t.Subtract(bill.Created)).TotalDays >= 1)
                        continue;
                    else
                    {
                        if (bill.Items != null && bill.Items.Count != 0)
                        {
                            foreach (var item in bill.Items)
                            {
                                foreach (var ytem in item.Ytems)
                                {
                                    alcograms = (float)(item.Drink.VolumeInMl * item.Drink.ABV * 0.01 * 0.789);
                                    elapsedTime = t.Subtract(ytem.Added);
                                    bac = (float)(halfbac * alcograms - (elapsedTime.TotalMinutes * 0.00025));
                                    if (bac > 0)
                                        totalbac = (float)(totalbac + bac);
                                }
                            }
                            promille = (float)(10 * totalbac);
                            sober = DateTime.Now.AddMinutes(totalbac / 0.00025);
                        }
                    }
                }
            }

            if (promille == 0)
                PromilleText = "You should be sober.";
            else
                PromilleText = "Around " + Math.Round((decimal)promille, 3) + "‰. Sober at " + sober.ToString("HH:mm:ss") + ", " + sober.ToString("dd.MM") + ".";
        }
    }
}
