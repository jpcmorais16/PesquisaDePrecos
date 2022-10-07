using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Atacadao;
using Domain;

namespace Data.Interfaces.SupermarketConnections
{
    public interface ISupermarketHttpConnector
    {
        public Task<List<Product>> SearchProductsOptimized(List<string> terms);

        public Task<List<Product>> SearchProducts(List<string> terms);
        public string GetName();

    }
}
