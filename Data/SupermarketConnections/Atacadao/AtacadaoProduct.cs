using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Atacadao
{
    public class AtacadaoProduct : IProduct
    {
        [Key]
        public int pk { get; set; }
        public string full_display { get; set; }
        public string category { get; set; }
        public AtacadaoPrice price { private get; set; }
        public AtacadaoPriceStatistics price_statistics { get; set; }
        public string name { get; set; }
        public Product GetProduct()
        {
            var teste = getPrice();
            var teste2 = Convert.ToDouble(price.price);
            return new Product
            {
                Price = Convert.ToDouble(price.price),
                Name = full_display,
                HasDiscount = price_statistics.Cheaper.Discount != 0,
                DomainName = "Atacadao"
            };
                
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            AtacadaoProduct product = obj as AtacadaoProduct;

            return Convert.ToDouble(getPrice()) < Convert.ToDouble(product.getPrice()) ? -1 : 1;
        }

        public string getPrice()
        {
            return price.price.Replace(',', '.');
        }
    }
}
