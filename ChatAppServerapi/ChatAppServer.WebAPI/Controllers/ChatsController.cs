using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Hubs;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ChatsController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ApplicationContext _context;

        public ChatsController(ApplicationContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats(string userId, string toUserId, CancellationToken cancellationToken)
        {
            List<Chat> chats = await _context.Chats
                .Where(p => ((p.UserId == userId && p.ToUserId == toUserId)) ||
                             (p.UserId == toUserId && p.ToUserId == userId))
                .OrderBy(p => p.Date)
                .ToListAsync(cancellationToken);

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto request, CancellationToken cancellationToken)
        {
            // Create a new chat message
            Chat chat = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.Message,
                Date = DateTime.Now
            };

            // Save the chat message to the database
            await _context.AddAsync(chat, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Retrieve the connection ID for the recipient
            var userConnection = await _context.UserConnections.FirstOrDefaultAsync(u => u.UserId == chat.UserId);

            string connectionId = userConnection.ConnectionId;

            if (connectionId != null)
            {
                // Send the message to the recipient
                await _hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.UserId, request.Message);
            }
            else
            {
                return NotFound(new { Message = "The recipient is not connected to the chat." });
            }

            return Ok(new { success = true , message = "Message sent has successfully!"});

        }
        
    }
}
