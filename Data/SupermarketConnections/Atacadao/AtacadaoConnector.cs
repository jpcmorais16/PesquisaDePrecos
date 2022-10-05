using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain;
using Data.Interfaces.SupermarketConnections;

namespace Data.Atacadao
{
    public class AtacadaoConnector : ISupermarketHttpConnector
    {
        public HttpClient _httpClient = new HttpClient();
        public string _baseLink = "https://www.atacadao.com.br/catalogo/search/?";

        public string GetName()
        {
            return "Atacadao";
        }

        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {

            string search = _baseLink + CreateQuery(searchTerms);

            var getResult = await _httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<AtacadaoResponse>(getResult);

            return response.results.Select(p => p.GetProduct()).ToList();


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
