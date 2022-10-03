using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product: IComparable
    {
        [Key]
        public int pk { get; set; } 
        public string category { get; set;}
        public Price price { get; set; }
        public PriceStatistics priceStatistics { get; set; }  
        public string name { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            Product product = obj as Product;

            return Convert.ToDouble(this.price.getPrice()) < Convert.ToDouble(product.price.price) ? -1 : 1;
        }
    }
}
