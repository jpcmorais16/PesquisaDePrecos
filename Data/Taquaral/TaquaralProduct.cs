using Domain;
using Domain.Interfaces;

namespace Data.Taquaral
{
    public class TaquaralProduct : IProduct
    {
        public string excerpt { get; set; }
        public List<TaquaralPrice> prices { get; set; }
        public TaquaralDiscount discount { get; set; }
        
        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public string getPrice()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct()
        {
            return new Product
            {
                Name = excerpt,
                Price = prices[0].price,
                HasDiscount = discount != null && discount.value != 0,
                DomainName = "Taquaral"

            };
        }
    }
}