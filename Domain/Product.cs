using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product : IComparable
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool HasDiscount { get; set; }
        public string DomainName { get; set; }
        public string Type { get; set; }
        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            Product product = obj as Product;

            return this.Price < product.Price ? -1 : 1;
        }

    }
}
