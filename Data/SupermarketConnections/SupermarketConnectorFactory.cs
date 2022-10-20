using Data.Atacadao;
using Data.Dalben;
using Data.Enxuto;
using Data.Taquaral;
using Services.Interfaces;
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
            
                new TaquaralConnector(),
                new EnxutoConnector(),     
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
