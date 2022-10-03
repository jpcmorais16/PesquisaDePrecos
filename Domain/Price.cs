namespace Domain
{
    public class Price
    {
        public string price { get; set; }

        public string getPrice()
        {
            return this.price.Replace(',', '.');
        }
    }
}