using Drink_Tracker.Model;
using System;

namespace Drink_Tracker.ViewModel
{
    public class TimestampViewModel : ViewModelBase
    {
        Timestamp timestamp;

        public TimestampViewModel(Timestamp t)
        {
            timestamp = t;
        }

        public Timestamp Timestamp
        {
            get { return timestamp; }
        }

        public String ItemName
        {
            get { return timestamp.Item.Drink.Name; }
        }

        public float ItemPrice
        {
            get { return timestamp.Item.DrinkPrice; }
        }

        public DateTime Added
        {
            get { return timestamp.Added; }
            set
            {
                timestamp.Added = value;
                NotifyPropertyChanged();
            }
        }
    }
}
