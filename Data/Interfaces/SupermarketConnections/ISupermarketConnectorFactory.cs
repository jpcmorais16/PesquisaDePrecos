using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.SupermarketConnections
{
    public interface ISupermarketConnectorFactory
    {
        public List<ISupermarketHttpConnector> GetConnectors();
    }
}
