using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SnackisApp.Data;
using SnackisApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class SendMessageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SendMessageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Message Message { get; set; }

        [BindProperty]
        public string RecipientId { get; set; }

        public SelectList Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = _userManager.Users.Select(u => new { u.Id, u.UserName }).ToList();
            Users = new SelectList(users, "Id", "UserName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var users = _userManager.Users.Select(u => new { u.Id, u.UserName }).ToList();
            Users = new SelectList(users, "Id", "UserName");

            var sender = await _userManager.GetUserAsync(User);
            var recipient = await _userManager.FindByIdAsync(RecipientId);

            if (sender == null || recipient == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid sender or recipient.");
                return Page();
            }

            // Assigning SenderId and RecipientId
            Message.SenderId = sender.Id;
            Message.RecipientId = RecipientId;
            Message.DateSent = DateTime.Now;

            // Remove validation errors for Sender, Recipient, SenderId, and RecipientId
            ModelState.Remove("Message.Sender");
            ModelState.Remove("Message.Recipient");
            ModelState.Remove("Message.SenderId");
            ModelState.Remove("Message.RecipientId");

            // Validate the model explicitly
            if (!TryValidateModel(Message, nameof(Message)))
            {
                // Log ModelState errors if any
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return Page();
            }

            // Add Message to context and save
            _context.Message.Add(Message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes: " + ex.Message);
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
