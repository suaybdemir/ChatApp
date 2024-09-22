using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppServer.WebAPI.Hubs
{
    public sealed class ChatHub(ApplicationContext _context) : Hub
    {
        public static Dictionary<string, string> Users = new();
        public async Task Connect(string UserId)
        {
            Users.Add(UserId, Context.ConnectionId);
            ApplicationUser? user = await _context.Users.FindAsync(UserId);

            if (user is not null)
            {
                user.Status = true;
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("Users",user);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? userId;
            Users.TryGetValue(Context.ConnectionId,out userId);

            ApplicationUser? user = await _context.Users.FindAsync(userId);
            if (userId is not null)
            {
                user!.Status = false;
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("Users", user);
            }
        }
    }
}
