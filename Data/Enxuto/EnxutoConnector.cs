using Data.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enxuto
{
    public class EnxutoConnector : IHttpConnector
    {

        public Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
            throw new NotImplementedException();
        }
    }
}
