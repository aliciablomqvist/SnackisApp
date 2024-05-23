using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SnackisUser> _userManager;

        public MessagesModel(ApplicationDbContext context, UserManager<SnackisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Message> ReceivedMessages { get; set; }
        public List<Message> SentMessages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            ReceivedMessages = await _context.Message
                .Include(m => m.Sender)
                .Where(m => m.RecipientId == user.Id)
                .ToListAsync();

            SentMessages = await _context.Message
                .Include(m => m.Recipient)
                .Where(m => m.SenderId == user.Id)
                .ToListAsync();

            return Page();
        }
    }
}
