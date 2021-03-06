﻿using Drink_Tracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Drink_Tracker.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        Item item;

        public ItemViewModel(Item i)
        {
            item = i;
            timestamps = new ObservableCollection<TimestampViewModel>(item.Timestamps.Select(timestamp => new TimestampViewModel(timestamp)));
            expanded = false;
        }

        private ObservableCollection<TimestampViewModel> timestamps;
        public ObservableCollection<TimestampViewModel> Timestamps
        {
            get { return timestamps; }
            set
            {
                timestamps = value;
                NotifyPropertyChanged();
            }
        }

        public Item Item
        {
            get { return item; }
        }

        public float TotalPrice
        {
            get { return BoughtCount == 0 ? 0 : BoughtCount * LastPrice; }
        }

        public string DrinkName
        {
            get { return item.Drink.Name; }
        }
        
        public float LastPrice
        {
            get { return item.Timestamps.OrderByDescending(t => t.Added).First().Item.DrinkPrice; }
        }

        public DateTime LastAdded
        {
            get { return item.Timestamps.OrderByDescending(t => t.Added).First().Added; }
        }
        
        public int BoughtCount
        {
            get { return item.Timestamps.Count; }
        }
        
        private bool expanded;
        public bool Expanded
        {
            get { return expanded; }
            set
            {
                expanded = value;
                NotifyPropertyChanged();
            }
        }
    }
}
