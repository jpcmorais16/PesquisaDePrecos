using Data.Interfaces;
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
    public class EnxutoConnector : IHttpConnector
    {
        public string _token { get; set; }
        public HttpClient _httpClient = new HttpClient();
        public string _baseLink = "https://api.enxuto.com/v1/loja/buscas/produtos/filial/1/centro_distribuicao/1/termo/";
        public string _authLink = "https://api.enxuto.com/v1/auth/loja/login";
        public string _key = "df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469";
        public string _username = "loja";
        public string _domain = "enxuto.com";
        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
            string search = _baseLink + CreateQuery(searchTerms);

            var token = await GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            
            var getResult = await _httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<EnxutoResponse>(getResult);

            return response.data.produtos.Select(p => p.GetProduct()).ToList();
            

            
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

        private async Task<string> GetTokenAsync()
        {

            _httpClient.BaseAddress = new Uri("https://api.enxuto.com/");

            var request = new
            {
                domain = "superdalben.com.br",
                username = "loja",
                key = "df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469"
            };

            var response = await _httpClient.PostAsJsonAsync("/v1/auth/loja/login", request);

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
