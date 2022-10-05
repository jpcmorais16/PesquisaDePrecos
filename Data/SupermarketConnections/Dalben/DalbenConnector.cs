using Data.Atacadao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Data.Interfaces.SupermarketConnections;

namespace Data.Dalben
{
    public class DalbenConnector : ISupermarketHttpConnector
    {
        //https://api.superdalben.com.br/v1/auth/loja/login
        //df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469
        //username: "loja"


        public string _token { get; set; }
        public HttpClient _httpClient = new HttpClient();
        public string _baseLink = "https://api.superdalben.com.br/v1/loja/buscas/produtos/filial/1/centro_distribuicao/1/termo/";
        public string _authLink = "https://api.superdalben.com.br/v1/auth/loja/login";
        public string _key = "df072f85df9bf7dd71b6811c34bdbaa4f219d98775b56cff9dfa5f8ca1bf8469";
        public string _username = "loja";
        public string _domain = "superdalben.com.br";
        public async Task<List<Product>> SearchProducts(List<string> searchTerms)
        {
            string search = _baseLink + CreateQuery(searchTerms);

            var token = await GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var getResult = await _httpClient.GetStringAsync(search);

            var response = JsonConvert.DeserializeObject<DalbenResponse>(getResult);

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
           
            _httpClient.BaseAddress = new Uri("https://api.superdalben.com.br/");

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
            return "Dalben";
        }
    }
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
    }
}

