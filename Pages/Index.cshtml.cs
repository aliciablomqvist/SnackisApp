using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SnackisApp.Models;
using SnackisApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DailyphilosopherService _dailyphilosopherService;
          private readonly DiscussionService _discussionService;

        public IndexModel(ILogger<IndexModel> logger, DailyphilosopherService dailyphilosopherService, DiscussionService discussionService)
        {
            _logger = logger;
            _dailyphilosopherService = dailyphilosopherService;
             _discussionService = discussionService;
        }
        public List<Post> Discussions { get; set; }
        public List<Philosopher> Dailyphilosophers { get; set; }
        public Philosopher RandomPhilosopher { get; set; }

        public async Task OnGetAsync()
        {
            Discussions = await _discussionService.GetDiscussionsAsync();
              Discussions = Discussions.OrderByDescending(d => d.Date).ToList();

            Dailyphilosophers = await _dailyphilosopherService.GetDailyphilosophersAsync();
            if (Dailyphilosophers != null && Dailyphilosophers.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(Dailyphilosophers.Count);
                RandomPhilosopher = Dailyphilosophers[index];
            }
        }
    }
}
