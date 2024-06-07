using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SnackisApp.Models; // Kontrollera att namnrymden är korrekt

namespace SnackisApp.Services
{
    public class DailyphilosopherService
    {
        private readonly HttpClient _httpClient;

        public DailyphilosopherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        // Method to fetch discussions from the API
        public async Task<List<Philosopher>> GetDailyphilosophersAsync()
        {
            // Fetch discussions from the local API
            //var response = await _httpClient.GetAsync("http://localhost:5004/api/Dailyphilosophers");

            // Fetch discussions from the Azure API
            var response = await _httpClient.GetAsync("https://mindfulmovementapi.azurewebsites.net/api/Dailyphilosophers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Philosopher>>();
        }
    }
}