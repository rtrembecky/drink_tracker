using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class BillViewModel : ViewModelBase
    {
        Bill bill;

        public BillViewModel(Bill b)
        {
            bill = b;
            edited = false;
            editField = Name;
            createdText = Created.ToString("HH:mm:ss") + ", " + Created.ToString("dd.MM");
        }

        public Bill Bill
        {
            get { return bill; }
        }

        public string Name
        {
            get { return bill.Name; }
            set
            {
                bill.Name = value;
                    NotifyPropertyChanged();
            }
        }

        private string editField;
        public string EditField
        {
            get { return editField; }
            set
            {
                editField = value;
                NotifyPropertyChanged();
            }
        }

        private String createdText;
        public String CreatedText
        {
            get { return createdText; }
            set
            {
                createdText = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime Created
        {
            get { return bill.Created; }
            set
            {
                bill.Created = value;
                NotifyPropertyChanged();
            }
        }

        public List<Item> Items
        {
            get { return bill.Items; }
            set
            {
                bill.Items = value;
                NotifyPropertyChanged();
            }
        }
        
        private bool edited;
        public bool Edited
        {
            get { return edited; }
            set
            {
                edited = value;
                NotifyPropertyChanged();
            }
        }
    }
}
