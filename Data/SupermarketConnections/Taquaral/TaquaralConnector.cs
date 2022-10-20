using Data.SupermarketConnections.Taquaral.TaquaralJSON;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Data.Taquaral
{
    public class TaquaralConnector : ISupermarketHttpConnector
    {

        
        private string _baseLink = "https://www.sitemercado.com.br/api/b2c/product?store_id=628&text=";
        private List<Product> _lastSearch;
        private string _lastMainTerm;

        public List<Product> GetLastSearch()
        {
            if (_lastSearch == null) throw new Exception("não há última pesquisa");

            return _lastSearch;

        }

        public string GetName()
        {
            return "Taquaral";
        }

        public async Task<List<Product>> SearchProductsOptimized(List<string> terms)
        {
            if (_lastMainTerm != null && terms.First().ToLower().Equals(_lastMainTerm.ToLower()))
                return _lastSearch;

            HttpClient httpClient = new HttpClient();
            string search = _baseLink + CreateQuery(terms);

            var header = new
            {
                Location = new { Latitude = -23.563776, Longitude = -46.6623916 },
                IdClientAdress = 0,
                IsDelivery = false,
                IdLoja = 628,
                IdRede = 556,
                DateBuild = DateTime.Now
            };

            var headerJson = JsonConvert.SerializeObject(header);

            httpClient.DefaultRequestHeaders.Add("sm-token", headerJson);
            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<TaquaralResponse>(getResult);

            _lastMainTerm = terms.First();
            _lastSearch =  response.products.Select(p => p.GetProduct()).ToList();

            return _lastSearch;
        }

        private string CreateQuery(List<string> searchTerms)
        {
            //tempero%20pronto%20500g
            string query = "";

            for (int i = 0; i < searchTerms.Count; i++)
            {
                query += searchTerms[i] + (i != searchTerms.Count - 1 ? "%20" : "");
            }
            return query;

        }
        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
       
            HttpClient httpClient = new HttpClient();
            string search = _baseLink + CreateQuery(searchTerms);

            var header = new
            {
                Location = new { Latitude = -23.563776, Longitude = -46.6623916 },
                IdClientAdress = 0,
                IsDelivery = false,
                IdLoja = 628,
                IdRede = 556,
                DateBuild = DateTime.Now
            };

            var headerJson = JsonConvert.SerializeObject(header);

            httpClient.DefaultRequestHeaders.Add("sm-token", headerJson);
            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<TaquaralResponse>(getResult);

            _lastMainTerm = searchTerms.First();
            _lastSearch = response.products.Select(p => p.GetProduct()).ToList();

            return _lastSearch;
        }

       
    }
}
