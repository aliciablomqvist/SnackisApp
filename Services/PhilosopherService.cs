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

        public async Task<List<Philosopher>> GetDailyphilosophersAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5003/api/Dailyphilosophers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Philosopher>>();
        }
    }
}

