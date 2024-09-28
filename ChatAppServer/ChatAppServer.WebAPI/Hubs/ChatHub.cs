using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ChatAppServer.WebAPI.Hubs
{
    public sealed class ChatHub: Hub
    {
        private readonly ApplicationContext _context;

        public ChatHub(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Connect(string UserId)
        {
            UserConnection? userConnection = await _context.UserConnections.FirstOrDefaultAsync(uc=>uc.UserId == UserId);

            if (userConnection == null)
            {
                UserConnection newConnection = new UserConnection
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = UserId,
                    ConnectionId = Context.ConnectionId
                };
                await _context.UserConnections.AddAsync(newConnection);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Update the existing connection
                userConnection.ConnectionId = Context.ConnectionId;
            }


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
            var user = _context.UserConnections.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);

            if (!string.IsNullOrEmpty(user.UserId))
            {
                var dbUser = await _context.Users.FindAsync(user.UserId);
                if (dbUser != null)
                {
                    dbUser.Status = false; // Mark the user as offline
                    await _context.SaveChangesAsync();

                    // Notify all clients about the updated user status
                    await Clients.All.SendAsync("Users", dbUser);
                }

                await _context.UserConnections.FirstOrDefaultAsync(u => u.ConnectionId == user.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
