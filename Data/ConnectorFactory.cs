using Data.Atacadao;
using Data.Dalben;
using Data.Enxuto;
using Data.Interfaces;
using Data.Taquaral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ConnectorFactory: IConnectorFactory
    {

        private List<IHttpConnector> _connectorList = new List<IHttpConnector>
        {
            new EnxutoConnector(),
            new TaquaralConnector(),
            new DalbenConnector(),
            new AtacadaoConnector()
            
        };

        public ConnectorFactory(){}

        public List<IHttpConnector> GetConnectors()
        {
            return _connectorList;
        }

    }
}
