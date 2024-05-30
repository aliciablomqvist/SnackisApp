using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    [Authorize]
    public class GroupPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SnackisUser> _userManager;

        public GroupPageModel(ApplicationDbContext context, UserManager<SnackisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Group> Groups { get; set; }
        public IList<GroupInvitation> Invitations { get; set; }

        [BindProperty]
        public string GroupName { get; set; }

        [BindProperty]
        public int GroupId { get; set; }

        [BindProperty]
        public string InvitedUserId { get; set; }

        [BindProperty]
        public string MessageContent { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Groups = await _context.Groups
                .Include(g => g.Members)
                    .ThenInclude(m => m.User)
                .Include(g => g.Messages)
                    .ThenInclude(m => m.User)
                .Where(g => g.CreatorId == user.Id || g.Members.Any(m => m.UserId == user.Id))
                .ToListAsync();
            
            Invitations = await _context.GroupInvitations
                .Include(i => i.Group)
                .Include(i => i.InvitingUser)
                .Where(i => i.InvitedUserId == user.Id && !i.Accepted)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCreateGroupAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var group = new Group
            {
                Name = GroupName,
                CreatorId = user.Id,
                Creator = user
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostInviteUserAsync()
        {
            var invitingUser = await _userManager.GetUserAsync(User);
            var invitation = new GroupInvitation
            {
                GroupId = GroupId,
                InvitedUserId = InvitedUserId,
                InvitingUserId = invitingUser.Id,
                Timestamp = DateTime.Now,
                Accepted = false
            };
            _context.GroupInvitations.Add(invitation);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendMessageAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var message = new GroupMessage
            {
                GroupId = GroupId,
                UserId = user.Id,
                Content = MessageContent,
                Timestamp = DateTime.Now
            };
            _context.GroupMessages.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveMemberAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var group = await _context.Groups.Include(g => g.Creator).FirstOrDefaultAsync(g => g.Id == GroupId);
            if (group != null && group.CreatorId == user.Id)
            {
                var member = await _context.GroupMembers.FirstOrDefaultAsync(m => m.GroupId == GroupId && m.UserId == InvitedUserId);
                if (member != null)
                {
                    _context.GroupMembers.Remove(member);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAcceptInvitationAsync(int groupId, string invitedUserId)
        {
            var invitation = await _context.GroupInvitations.FindAsync(groupId, invitedUserId);
            if (invitation != null)
            {
                invitation.Accepted = true;
                _context.GroupMembers.Add(new GroupMember
                {
                    GroupId = invitation.GroupId,
                    UserId = invitation.InvitedUserId
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
