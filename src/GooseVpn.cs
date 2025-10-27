using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace GooseVpnApi
{
    public class GooseVpn
    {
        private string token;
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://api1.goosevpn.com";
        public GooseVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "okhttp/4.9.0");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Register(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                first_name = "Goose",
                last_name = "User",
                password = password
            });
            var response = await httpClient.PostAsync($"{apiUrl}/in_app/signup", data);
            var content = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("token", out var tokenElement))
            {
                token = tokenElement.GetString();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return content;
        }

        public async Task<string> GetAccountPlan()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/users/me/plan");
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetAccountInfo()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/users/me");
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetServers()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/users/me/servers");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
