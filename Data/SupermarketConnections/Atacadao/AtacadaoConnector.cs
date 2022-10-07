using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain;
using Data.Interfaces.SupermarketConnections;
using Data.SupermarketConnections.Atacadao.AtacadaoProduct;

namespace Data.Atacadao
{
    public class AtacadaoConnector : ISupermarketHttpConnector
    {
        public string _baseLink = "https://www.atacadao.com.br/catalogo/search/?";
        private List<Product> _lastSearch;
        private string _lastMainTerm;

        public List<Product> GetLastSearch()
        {
            return _lastSearch;
        }


        public string GetName()
        {
            return "Atacadao";
        }

        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {

            HttpClient httpClient = new HttpClient();

            string search = _baseLink + CreateQuery(searchTerms);

            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<AtacadaoResponse>(getResult);

            _lastMainTerm = searchTerms.First();
            _lastSearch = response.results.Select(p => p.GetProduct()).ToList();

            return _lastSearch;


        }
        public async Task<List<Product>> SearchProductsOptimized(List<string> terms)
        {
            if (_lastMainTerm != null && terms.First().ToLower().Equals(_lastMainTerm.ToLower()))
                return _lastSearch;

            HttpClient httpClient = new HttpClient();

            string search = _baseLink + CreateQuery(terms);

            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<AtacadaoResponse>(getResult);

            _lastMainTerm = terms.First();
            _lastSearch = response.results.Select(p => p.GetProduct()).ToList();

            return _lastSearch;


        }


        private string CreateQuery(List<string> searchTerms)
        {
            string query = "q=";

            for (int i = 0; i < searchTerms.Count; i++)
            {
                query += searchTerms[i] + (i != searchTerms.Count - 1 ? "%20" : "");

            }
            query += "&page=1";
            return query;

        }


    }
}
