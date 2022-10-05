using Data.Atacadao;
using Data.Dalben;
using Data.Enxuto;
using Data.Interfaces;
using Data.Interfaces.SupermarketConnections;
using Data.Taquaral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SupermarketConnections
{
    public class SupermarketConnectorFactory : ISupermarketConnectorFactory
    {

        private List<ISupermarketHttpConnector> _connectorList = new List<ISupermarketHttpConnector>
            {
                new EnxutoConnector(),
                new TaquaralConnector(),
                new DalbenConnector(),
                new AtacadaoConnector()

            };
        public SupermarketConnectorFactory() { }

        public List<ISupermarketHttpConnector> GetConnectors()
        {
            return _connectorList;

        }

    }
}
