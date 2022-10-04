using Data.Interfaces;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Taquaral
{
    public class TaquaralConnector : IHttpConnector
    {

        private HttpClient _httpClient = new HttpClient();
        private string _baseLink = "https://www.sitemercado.com.br/api/b2c/product?store_id=628&text=";
        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
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

            _httpClient.DefaultRequestHeaders.Add("sm-token", headerJson);
            var getResult = await _httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<TaquaralResponse>(getResult);

            return response.products.Select(p => p.GetProduct()).ToList();
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
    }
}
