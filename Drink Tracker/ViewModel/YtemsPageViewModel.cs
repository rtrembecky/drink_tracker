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
            ytemsList = manager.GetYtems(b);
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
        
        private ObservableCollection<YtemViewModel> ytemsList;
        public ObservableCollection<YtemViewModel> YtemsList
        {
            get { return ytemsList; }
            set
            {
                ytemsList = value;
                NotifyPropertyChanged();
            }
        }

        public int YtemsCount
        {
            get { return ytemsList.Count; }
        }

        public DateTime YtemsLastAdded
        {
            get { return ytemsList.OrderByDescending(y => y.Added).First().Added; }
        }

        public float YtemsTotalPrice
        {
            get { return ytemsList.Count == 0 ? 0 : ytemsList.Count * ytemsList.First().Item.DrinkPrice; }
        }
    }
}
