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

        public DiscussionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to fetch discussions from the API
        public async Task<List<Post>> GetDiscussionsAsync()
        {
            // Fetch discussions from the local API
            // var response = await _httpClient.GetAsync("http://localhost:5004/api/Discussions");

            // Fetch discussions from the Azure API
            var response = await _httpClient.GetAsync("https://mindfulmovementapi.azurewebsites.net/api/Discussions");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Post>>();

        }
    }
}