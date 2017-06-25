using Drink_Tracker.Model;

namespace Drink_Tracker.ViewModel
{
    class NewDrinkPageViewModel : ViewModelBase
    {
        public NewDrinkPageViewModel(BillAndType billAndType)
        {
            header = "New custom " + billAndType.Type;

            switch (billAndType.Type)
            {
                case "Beer":
                    abv = "4";
                    volume = "5";
                    cost = "30";
                    break;
                case "Wine":
                    abv = "12";
                    volume = "2";
                    cost = "40";
                    break;
                case "Shot":
                    abv = "38";
                    volume = "0.4";
                    cost = "40";
                    break;
                case "Nonalco":
                    abv = "0";
                    volume = "5";
                    cost = "30";
                    break;
                case "Cocktail":
                    abv = "15";
                    volume = "1.8";
                    cost = "80";
                    break;
                case "Other":
                    abv = "0";
                    volume = "0";
                    cost = "20";
                    break;
            }
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

        string abv;
        public string Abv
        {
            get { return abv; }
            set
            {
                abv = value;
                NotifyPropertyChanged();
            }
        }

        string volume;
        public string Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                NotifyPropertyChanged();
            }
        }

        string cost;
        public string Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                NotifyPropertyChanged();
            }
        }
    }
}
