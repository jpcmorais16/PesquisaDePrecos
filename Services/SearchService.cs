using Services.Interfaces;
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
        private ISupermarketConnectorFactory _connectorFactory;
        private Dictionary<string, List<string>> _filteredWords = new Dictionary<string, List<string>>();
        

        public SearchService(ISupermarketConnectorFactory connectorFactory)
        {
            _connectorFactory = connectorFactory;
        }

        public async Task<List<Product>> SearchItemsInAllConnectionsWithoutRestrictions(List<string> searchTerms)
        {

            var connectorList = _connectorFactory.GetConnectors();

           // List<List<Product>> matches = new List<List<Product>>();

            foreach (ISupermarketHttpConnector connector in connectorList)
            {
                try
                {

                    var products = await connector.SearchProducts(searchTerms);
                    //var bestMatchingProducts = products.OrderByDescending(p => HowManyMatchingWordsWithWeight(p, searchTerms)).ToList();

                    //bestMatchingProducts = bestMatchingProducts.Where(p => p.Name.ToLower().Split(" ").Contains(searchTerms.First().ToLower())).ToList();

                    //if (bestMatchingProducts.Count == 0) throw new Exception();

                    //return bestMatchingProducts;

                    return products;
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("Nenhum match encontrado");
                    
                }

               
            }
            throw new Exception("Not found");

            

        }

        public void CreateFilter(List<List<string>> allProducts)
        {
            //var lastMainWord = "";

            for(int i=0; i < allProducts.Count; i++)
            {
                var wordList = allProducts[i][1].ToLower().Split(" ");

                if (!_filteredWords.Keys.Contains(wordList.First()))
                {
                    _filteredWords[wordList.First()] = new List<string>();
                }

                foreach(string word in wordList)
                {
                    var str = word.Replace("(", "").Replace(")", "").ToString().ToLower();

                    if (!_filteredWords[wordList.First()].Contains(str) && !(word.ToLower().Equals("em") || word.ToLower().Equals("de")))
                        _filteredWords[wordList.First()].Add(str);
                }

            }

        }

        public async Task<List<Product>> SearchFilteredItemsInAllConnections(List<string> searchTerms)
        {

            var connectorList = _connectorFactory.GetConnectors();
            
            

            foreach (ISupermarketHttpConnector connector in connectorList)
            {
                

                try
                {
                    var products = await connector.SearchProductsOptimized(searchTerms);

                    if (products.Count == 0) throw new Exception();

                    var resposta = products.Where(p => !p.HasDiscount &&
                    searchTerms.Where(n => !(n.ToLower().Equals("em") || n.ToLower().Equals("de"))).All(t => p.Name.ToLower().Split(" ").Contains(t.ToLower()))).ToList();

                    resposta = resposta.Where(p => 
                        p.Name.ToLower().Split(" ").All(s => searchTerms.Contains(s) || !_filteredWords.Keys.Contains(s))).ToList();

                    resposta = resposta.Where(p =>
                        p.Name.ToLower().Split(" ").All(s => searchTerms.Contains(s) || !_filteredWords[searchTerms.First().ToLower()].Contains(s))).ToList();

                    //resposta = resposta.Where(p =>
                    //    !p.Name.ToLower().Split(" ").All(s => searchTerms.Contains(s) 
                    //    || (!_filteredWords.Keys.Contains(s) && !_filteredWords[searchTerms.First().ToLower()].Contains(s)) )).ToList();

                    if (resposta.Count == 0) throw new Exception();

                    resposta.Sort();

                    return resposta;
                }
                catch(Exception ex)
                {                  
                    var busca = string.Join("", searchTerms);
                    Console.WriteLine($"Nenhum resultado encontrado para {busca} na conexao com {connector.GetName()}");
                }

            }

            return null;

        }

       

        private int HowManyMatchingWordsWithWeight(Product p, List<string> searchTerms)
        {
            int result = 0;
            if (p.Name.ToLower().Split(" ").Contains(searchTerms.First().ToLower()))
                result += 3;

            foreach(string term in searchTerms.Skip(1))
            {
                if(p.Name.ToLower().Split(" ").Contains(term.ToLower()) || p.Type.ToLower().Contains(term.ToLower()))
                    result++;

            }
            return result;
        }

        private List<string> _specialWords = new List<string>
        {
            "barra",
            "club",
            "caldo",
            "extrato",
            "farinha",
            "molho",
        };



    }
}