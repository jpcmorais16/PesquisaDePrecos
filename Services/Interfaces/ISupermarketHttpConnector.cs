using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services.Interfaces
{
    public interface ISupermarketHttpConnector
    {
        public Task<List<Product>> SearchProductsOptimized(List<string> terms);

        public Task<List<Product>> SearchProducts(List<string> terms);
        public string GetName();

    }
}
