using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class DrinkViewModel : ViewModelBase
    {
        Drink drink;

        public DrinkViewModel(Drink d)
        {
            drink = d;
        }

        public Drink Drink
        {
            get { return drink; }
        }

        public string DrinkName
        {
            get { return drink.Name; }
            set
            {
                drink.Name = value;
                NotifyPropertyChanged();
            }
        }

        public string Type
        {
            get { return drink.Type; }
            set
            {
                drink.Type = value;
                NotifyPropertyChanged();
            }
        }

        public float ABV
        {
            get { return drink.ABV; }
            set
            {
                drink.ABV = value;
                NotifyPropertyChanged();
            }
        }

        public int VolumeInMl
        {
            get { return drink.VolumeInMl; }
            set
            {
                drink.VolumeInMl = value;
                NotifyPropertyChanged();
            }
        }

        public List<Price> Prices
        {
            get { return drink.Prices; }
            set
            {
                drink.Prices = value;
                NotifyPropertyChanged();
            }
        }

        public float FirstPrice
        {
            get { return drink.Prices.First().Value; }
            set
            {
                NotifyPropertyChanged();
            }
        }        
    }
}
