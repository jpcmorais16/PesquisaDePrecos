using Data.Interfaces.SupermarketConnections;
using Data.SupermarketConnections.Enxuto.EnxutoJSON;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enxuto
{
    public class EnxutoConnector : ISupermarketHttpConnector
    {
        private string _token { get; set; }
        private string _baseLink = "https://api.enxuto.com/v1/loja/buscas/produtos/filial/1/centro_distribuicao/1/termo/";
        private string _authLink = "https://api.enxuto.com/v1/auth/loja/login";
        private string _key = "df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469";
        private string _username = "loja";
        private string _domain = "enxuto.com";
        private List<Product> _lastSearch;
        private string _lastMainTerm;

        public List<Product> GetLastSearch()
        {
            return _lastSearch;
        }
        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
            
            HttpClient httpClient = new HttpClient();

            string search = _baseLink + CreateQuery(searchTerms);

            var token = await GetTokenAsync(httpClient);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
 
            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<EnxutoResponse>(getResult);

            _lastMainTerm = searchTerms.First();
            _lastSearch =  response.data.produtos.Select(p => p.GetProduct()).ToList();

            return _lastSearch;
            
            
        }
        private string CreateQuery(List<string> searchTerms)
        {
            string query = "";

            for (int i = 0; i < searchTerms.Count; i++)
            {
                query += searchTerms[i] + (i != searchTerms.Count - 1 ? "+" : "");
            }
            query += "?";
            return query;
        }
        public async Task<List<Product>> SearchProductsOptimized(List<string> terms)
        {
            if (_lastMainTerm != null && terms.First().ToLower().Equals(_lastMainTerm.ToLower()))
                return _lastSearch;

            HttpClient httpClient = new HttpClient();

            string search = _baseLink + CreateQuery(terms);

            var token = await GetTokenAsync(httpClient);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var getResult = await httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<EnxutoResponse>(getResult);

            _lastMainTerm = terms.First();
            _lastSearch = response.data.produtos.Select(p => p.GetProduct()).ToList();

            return _lastSearch;


        }
       

        private async Task<string> GetTokenAsync(HttpClient httpClient)
        {

            httpClient.BaseAddress = new Uri("https://api.enxuto.com/");

            var request = new
            {
                domain = "superdalben.com.br",
                username = "loja",
                key = "df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469"
            };

            var response = await httpClient.PostAsJsonAsync("/v1/auth/loja/login", request);

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return result.Data;

        }

        public string GetName()
        {
            return "Enxuto";
        }
    }
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
    }
}
