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
                

                try
                {
                    var products = await connector.SearchProducts(searchTerms);

                    if (products.Count == 0) throw new Exception();

                    var resposta = products.Where(p => !p.HasDiscount &&
                    searchTerms.All(t => p.Name.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    if (resposta.Count == 0) throw new Exception();

                    resposta.Sort();

                    return resposta;
                }
                catch(Exception ex)
                {
                    var busca = "";
                    foreach(string term in searchTerms)
                    {
                        busca += $"{term} ";
                    }
                    Console.WriteLine($"Nenhum resultado encontrado para {busca} na conexao com {connector.GetName()}");
                }

            }

            return null;

        }

        public async Task<List<Product>> SearchItemsInAllConnectionsWithWordRestrictions(List<string> searchTerms, List<string> restrictions)
        {

            var connectorList = _connectorFactory.GetConnectors();

            foreach (IHttpConnector connector in connectorList)
            {


                try
                {
                    var products = await connector.SearchProducts(searchTerms);

                    if (products.Count == 0) throw new Exception();

                    var resposta = products.Where(p => !p.HasDiscount &&
                    searchTerms.All(t => p.Name.Split(" ").Select(s => s.ToLower()).Contains(t))).ToList();

                    if (resposta.Count == 0) throw new Exception();

                    resposta.Sort();

                    return resposta;
                }
                catch (Exception ex)
                {
                    var busca = "";
                    foreach (string term in searchTerms)
                    {
                        busca += $"{term} ";
                    }
                    Console.WriteLine($"Nenhum resultado encontrado para {busca} na conexao com {connector.GetName()}");
                }

            }

            return null;

        }



    }
}