using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IHttpConnector
    {
        public Task<Response> SearchProducts(List<string> searchTerms);
    }
}
