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
                var products = await connector.SearchProducts(searchTerms);

                if (products != null)
                {
                    var resposta = products.Where(p => !p.HasDiscount &&
                    searchTerms.All(t => p.Name.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

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
                var products = await connector.SearchProducts(searchTerms);

                if (products != null)
                {
                    var resposta = products.Where(p => !p.HasDiscount &&
                    searchTerms.All(t => p.Name.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    resposta = resposta.Where(p =>
                    !restrictions.Any(t => p.Name.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    resposta.Sort();

                    return resposta;
                }

            }

            return null;

        }



    }
}