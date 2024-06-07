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
        public IList<Post> RecentDiscussions { get; set; } 
        public List<Philosopher> Dailyphilosophers { get; set; }
        public Philosopher RandomPhilosopher { get; set; }

        public async Task OnGetAsync()
        {
            //Innan ändringar
          //  Discussions = await _discussionService.GetDiscussionsAsync();
            //  Discussions = Discussions.OrderByDescending(d => d.Date).ToList();

             /*Discussions = await _discussionService.GetDiscussionsAsync();
 if (Discussions != null && Discussions.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(Discussions.Count);
                RandomDiscussions = Discussions[index];
            }*/
Discussions = await _discussionService.GetDiscussionsAsync();
    if (Discussions != null && Discussions.Count > 0)
    {
        Discussions = Discussions.OrderByDescending(d => d.Date).ToList();

        RecentDiscussions = Discussions.Take(4).ToList();


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
}