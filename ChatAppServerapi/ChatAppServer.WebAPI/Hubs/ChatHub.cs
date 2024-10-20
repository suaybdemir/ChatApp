using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Hubs
{
    public sealed class ChatHub : Hub
    {
        private readonly ApplicationContext _context;

        public ChatHub(ApplicationContext context)
        {
            _context = context;
        }

        // User connects to the SignalR hub
        public async Task Connect(string UserId)
        {
            var userConnection = await _context.UserConnections
                .FirstOrDefaultAsync(uc => uc.UserId == UserId);

            if (userConnection == null)
            {
                // Create new connection if not exists
                var newConnection = new UserConnection
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
                // Update the existing connection with the new ConnectionId
                userConnection.ConnectionId = Context.ConnectionId;
                await _context.SaveChangesAsync();
            }

            // Set user status as online
            var user = await _context.Users.FindAsync(UserId);
            if (user != null)
            {
                user.Status = true;  // Mark as online
                await _context.SaveChangesAsync();

                // Notify all clients that the user's status is online
                await Clients.All.SendAsync("Users", user);
            }
        }

        // Handle disconnection logic
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userConnection = await _context.UserConnections
                .FirstOrDefaultAsync(u => u.ConnectionId == Context.ConnectionId);

            if (userConnection != null)
            {
                var user = await _context.Users.FindAsync(userConnection.UserId);
                if (user != null)
                {
                    user.Status = false;  // Mark as offline
                    await _context.SaveChangesAsync();

                    // Notify all clients about the user's status change
                    await Clients.All.SendAsync("Users", user);
                }

                // Remove the connection record from the database
                _context.UserConnections.Remove(userConnection);
                await _context.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Method to broadcast the message to all clients in real time
        public async Task SendMessage(string user, string message)
        {
            // Broadcast message to all clients
            Chat chat = new()
            {
                Id = Guid.NewGuid().ToString(),
                ToUserId = "3d57495a-e4ac-4e6b-a946-bc475327d7aa",
                UserId = user,
                Message = message,
                Date = DateTime.Now
            };

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
