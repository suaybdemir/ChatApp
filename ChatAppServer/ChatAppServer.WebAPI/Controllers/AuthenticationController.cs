﻿using ChatAppServer.WebAPI.Data;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthenticationController(ApplicationContext _context,
        IWebHostEnvironment _env): ControllerBase
    { 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request,CancellationToken cancellationToken)
        {
            bool isNameExists = await _context.Users.AnyAsync(p=>p.Name == request.Name,cancellationToken);

            if (isNameExists)
            {
                return BadRequest(new { Message = "This username has used!" });

            }

            if (request.Avatar == null || request.Avatar.Length == 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Dosya yüklenmedi.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var uploadsFolder = Path.Combine(_env.WebRootPath, "User");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, request.Avatar.FileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Avatar.CopyToAsync(stream);
                }
            }
            catch (IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured when uploading image: " + ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }

            ApplicationUser user = new()
            { 
                Name = request.Name,
                AvatarPath = filePath
            };

            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string name,CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(p=>p.Name == name,cancellationToken);

            if(user is null)
            {
                return BadRequest(new { Message = "User couldn't find." });
            }

            user.Status = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(user);
        }
    }
}