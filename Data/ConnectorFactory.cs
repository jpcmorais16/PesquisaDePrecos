using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ConnectorFactory: IConnectorFactory
    {

        private List<IHttpConnector> _connectorList = new List<IHttpConnector>();
        private int _connectorIndex = 0;

        public ConnectorFactory()
        {
            _connectorList.Add(new AtacadaoConnector());
        }

        public List<IHttpConnector> GetConnectors()
        {
            return _connectorList;
        }

    }
}
