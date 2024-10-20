using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Hubs;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(ApplicationContext _context,
        IWebHostEnvironment _env,
        IHubContext<ChatHub> _hubContext) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetConnectedUsersAll(CancellationToken cancellationToken)
        {
            // Retrieve connected users ordered by EntryDate
            List<UserConnection> userConnections = await _context.UserConnections
                                                                 .OrderBy(p => p.EntryDate)
                                                                 .ToListAsync(cancellationToken);

            // Check if there are no connected users
            if (userConnections.Count == 0)
            {
                return BadRequest(new { Message = "There aren't any connected users." });
            }

            // Extract the list of userIds from the UserConnections
            var userIds = userConnections.Select(uc => uc.UserId).ToList();

            // Retrieve the users from the Users table whose Id matches the userIds in UserConnections
            List<ApplicationUser> users = await _context.Users
                                                         .Where(u => userIds.Contains(u.Id))
                                                         .ToListAsync(cancellationToken);

            // Return the list of connected users
            return Ok(users);
        }

    }
}
