using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Threading.Tasks;

namespace SnackisApp.Helpers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SnackisUser> _userManager;

        public ReactionsController(ApplicationDbContext context, UserManager<SnackisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: Add a new reaction to a post
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] int postId, [FromForm] string reactionType)
        {
            // Get the current user's Id
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            // Creates a new reaction
            var reaction = new Reaction
            {
                PostId = postId,
                ReactedbyId = userId,
                ReactionType = reactionType,
                CreatedAt = DateTime.Now
            };

            // Adds the reaction to the database
            _context.Reactions.Add(reaction);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
