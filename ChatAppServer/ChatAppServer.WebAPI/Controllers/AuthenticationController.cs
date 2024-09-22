using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthenticationController(ApplicationContext _context,
        IWebHostEnvironment _env
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
        public async Task<IActionResult> Login(string name,CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(p=>p.Name == name,cancellationToken);

            if(user is null)
            {
                return BadRequest(new { Message = "User couldn't find." });
            }

            user.Status = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(new { success = true, user });
        }
    }
}
