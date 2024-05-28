using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisApp.Models;
using System.Collections.Generic;


namespace SnackisApp.Services
{
    public class DiscussionService
    {
        private readonly HttpClient _httpClient;

        public DiscussionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetDiscussionsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5003/Api/Discussions"); // Använd rätt URL till ditt API
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Post>>();
        }
    }
}
