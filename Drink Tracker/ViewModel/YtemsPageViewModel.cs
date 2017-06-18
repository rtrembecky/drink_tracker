using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class YtemsPageViewModel : ViewModelBase
    {
        DatabaseManager manager;
        float totalPrice;

        public YtemsPageViewModel(Bill b)
        {
            ytemsHeaderTitleText = b.Name + " bill";

            Calculation(b);

            toPayText = "To pay: " + totalPrice.ToString("0.00") + " CZK";

            DatabaseManager manager = new DatabaseManager();
            ytems = manager.ShowYtems(b);
        }

        private void Calculation(Bill b)
        {
            totalPrice = (float)0;
            foreach (var i in b.Items)
            {
                totalPrice += i.DrinkPrice * i.Ytems.Count;
            }
        }

        string ytemsHeaderTitleText;
        public string YtemsHeaderTitleText
        {
            get { return ytemsHeaderTitleText; }
            set
            {
                ytemsHeaderTitleText = value;
                NotifyPropertyChanged();
            }
        }

        string toPayText;
        public string ToPayText
        {
            get { return toPayText; }
            set
            {
                toPayText = value;
                NotifyPropertyChanged();
            }
        }

        string ytemsList;
        public string YtemsList
        {
            get { return ytemsList; }
            set
            {
                ytemsList = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<YtemViewModel> ytems;
        public ObservableCollection<YtemViewModel> Ytems
        {
            get { return ytems; }
            set
            {
                ytems = value;
                NotifyPropertyChanged();
            }
        }
    }
}
