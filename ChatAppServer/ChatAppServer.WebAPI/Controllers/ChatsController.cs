using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Hubs;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ChatsController(ApplicationContext _context,IHubContext<ChatHub> _hubContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChats(string userId,string toUserId,CancellationToken cancellationToken)
        {
            List<Chat> chats = await _context.Chats.Where(p =>
            p.UserId == userId && p.ToUserId == toUserId ||
            p.UserId == toUserId && p.ToUserId == userId)
            .OrderBy(p=>p.Date)
            .ToListAsync(cancellationToken);

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto request,CancellationToken cancellationToken)
        {
            Chat chat = new()
            {
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.Message,
                Date = DateTime.Now
            };

            await _context.AddAsync(chat,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            string connectionId = ChatHub.Users.First(p => p.Value == chat.ToUserId).Key;

            await _hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);

            return Ok();
        }
    }
}
