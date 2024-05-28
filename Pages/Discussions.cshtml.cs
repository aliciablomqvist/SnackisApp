using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Models;
using SnackisApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class DiscussionsModel : PageModel
    {
        private readonly DiscussionService _discussionService;

        public DiscussionsModel(DiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        public List<Post> Discussions { get; set; }

        public async Task OnGetAsync()
        {
            Discussions = await _discussionService.GetDiscussionsAsync();
        }
    }
}
