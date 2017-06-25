namespace Drink_Tracker.Model
{
    public class BillAndType
    {
        public Bill Bill { get; set; }
        public string Type { get; set; }
    }

    public class DrinkAndPrice
    {
        public Drink Drink { get; set; }
        public Price Price { get; set; }
    }
}
