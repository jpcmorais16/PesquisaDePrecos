using Data.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SearchService
    {
        IConnectorFactory _connectorFactory;

        public SearchService(IConnectorFactory connectorFactory)
        {
            _connectorFactory = connectorFactory;
        }

        public async Task<List<Product>> SearchItemsInAllConnectionsWithoutRestrictions(List<string> searchTerms)
        {

            var connectorList = _connectorFactory.GetConnectors();

            foreach (IHttpConnector connector in connectorList)
            {
                var response = await connector.SearchProducts(searchTerms);

                if (response != null)
                {
                    var resposta = response.results.Where(p => p.price_statistics.Cheaper.Discount == 0 &&
                    searchTerms.All(t => p.full_display.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    resposta.Sort();

                    return resposta;
                }

            }

            return null;

        }

        public async Task<List<Product>> SearchItemsInAllConnectionsWithWordRestrictions(List<string> searchTerms, List<string> restrictions)
        {

            var connectorList = _connectorFactory.GetConnectors();

            foreach (IHttpConnector connector in connectorList)
            {
                var response = await connector.SearchProducts(searchTerms);

                if (response != null)
                {
                    var resposta = response.results.Where(p => p.price_statistics.Cheaper.Discount == 0 &&
                    searchTerms.All(t => p.full_display.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    resposta = resposta.Where(p =>
                    !restrictions.Any(t => p.full_display.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    resposta.Sort();

                    return resposta;
                }

            }

            return null;

        }



    }
}