using SnackisApp.Models;
using Microsoft.Extensions.Configuration;

namespace SnackisApp.Services
{
	public class DailyphilosopherService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _config;

		public DailyphilosopherService(HttpClient httpClient, IConfiguration config)
		{
			_httpClient = httpClient;
			_config = config;
		}

		public async Task<List<Philosopher>> GetDailyphilosophersAsync()
		{
			var baseUrl = _config["ApiBaseUrl"];
			var response = await _httpClient.GetAsync($"{baseUrl}/api/Dailyphilosophers");

			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<List<Philosopher>>();
		}
	}
}
