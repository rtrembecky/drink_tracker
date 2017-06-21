using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drink_Tracker.ViewModel
{
    public class YtemViewModel : ViewModelBase
    {
        Ytem ytem;

        public YtemViewModel(Ytem y)
        {
            ytem = y;
        }

        public Ytem Ytem
        {
            get { return ytem; }
        }

        public DateTime Added
        {
            get { return ytem.Added; }
            set
            {
                ytem.Added = value;
                NotifyPropertyChanged();
            }
        }
    }
}
