
using SnackisApp.Models;
using Microsoft.Extensions.Configuration;

namespace SnackisApp.Services
{
	public class DiscussionService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _config;

		public DiscussionService(HttpClient httpClient, IConfiguration config)
		{
			_httpClient = httpClient;
			_config = config;
		}

		public async Task<List<Post>> GetDiscussionsAsync()
		{
			var baseUrl = _config["ApiBaseUrl"];
			var response = await _httpClient.GetAsync($"{baseUrl}/api/Discussions");

			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<List<Post>>();
		}
	}
}
