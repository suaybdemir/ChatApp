using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Hubs;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthenticationController(ApplicationContext _context,
        IWebHostEnvironment _env,
        IHubContext<ChatHub> _hubContext
        ): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
        {
            
            bool isNameExists = await _context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (isNameExists)
            {
                return BadRequest(new { Message = "This username has been used!" });
            }

            string filePath = string.Empty;

            if (request.Avatar is not null )
            {
                if(request.Avatar.Length != 0)
                {
                    var uploadsFolder = Path.Combine("/StaticFiles", "Users", request.Name);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Avatar.FileName);
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Avatar.CopyToAsync(stream);
                    }
                }
                
            }

            ApplicationUser user = new()
            {
                Name = request.Name,
                AvatarPath = filePath
            };

            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync();
            
            return Ok(new { Message = "Registration successful", Name = request.Name });
        }


        [HttpPost]
        public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { Message = "Username is required." });
            }

            ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

            if (user is null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            // Check if the user is already connected
            var existingConnection = await _context.UserConnections
                .FirstOrDefaultAsync(u => u.UserId == user.Id, cancellationToken);

            if (existingConnection != null)
            {
                return BadRequest(new { Message = "User is already connected." });
            }

            user.Status = true;

            UserConnection newConnection = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ConnectionId = Guid.NewGuid().ToString()
            };

            _context.UserConnections.Add(newConnection);

            await _hubContext.Clients.Client(newConnection.ConnectionId)
        .SendAsync("Users", user);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(new { success = true, user });
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { Message = "Username is required." });
            }

            ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

            if (user is null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            // Kullanıcının bağlantısını al
            var existingConnection = await _context.UserConnections
                .FirstOrDefaultAsync(u => u.UserId == user.Id, cancellationToken);

            if (existingConnection == null)
            {
                return Ok(new { Message = "User is not connected." });
            }

            // Bağlantıyı ve kullanıcıyı güncelle
            _context.UserConnections.Remove(existingConnection);
            user.Status = false;

            // Veritabanı değişikliklerini kaydet
            await _context.SaveChangesAsync(cancellationToken);

            // Kullanıcıya, bağlantısının kesildiği bilgisini gönder
            await _hubContext.Clients.Client(existingConnection.ConnectionId)
                .SendAsync("Logout", user);

            return Ok(new { success = true, message = "User logged out successfully." });
        }


    }
}
