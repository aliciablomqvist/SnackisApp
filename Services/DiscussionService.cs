using System.Collections.Generic;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SnackisApp.Data;


namespace SnackisApp.Services
{
    public class DiscussionService
    {
        private readonly HttpClient _httpClient;
        // private readonly ApplicationDbContext _context;

        public DiscussionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetDiscussionsAsync()
        {
            //Ändra detta till API från Azure
            var response = await _httpClient.GetAsync("http://localhost:5004/api/Discussions"); // Använd rätt URL till ditt API
            response.EnsureSuccessStatusCode();
            //Innan ändingar
            return await response.Content.ReadFromJsonAsync<List<Post>>();

        }
    }
}
