using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data
{
    public class AtacadaoItemQuery
    {
        public HttpClient _httpClient = new HttpClient();
        public string _baseLink = "https://www.atacadao.com.br/catalogo/search/?";

        public async Task<List<Product>> SearchProducts(List<string> searchTerms) 
        {

            //string search = _baseLink + CreateQuery(searchTerms);
            string search = "https://www.atacadao.com.br/catalogo/search/?q=suco%20100ml&page=1&order_by=-relevance";

            var getResult = await _httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<Response>(getResult);

            return response.results;

        }

        private string CreateQuery(List<string> searchTerms)
        {
            string query = "q=";

            for(int i=0; i < searchTerms.Count; i++)
            {
                query += searchTerms[i] + (i != searchTerms.Count - 1 ? "%20" : "");

            }
            query += "&page=1";
            return query;

        }


    }
}
