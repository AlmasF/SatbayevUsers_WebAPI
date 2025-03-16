using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SatbayevUsers_WebAPI.Data;
using System.Net;

namespace SatbayevUsers_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(SatbayevUsersDbContext context) : ControllerBase
    {
        private readonly SatbayevUsersDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User newUser)
        {
            if (newUser is null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.IIN == newUser.IIN || u.Email == newUser.Email);

            if (user is not null)
            {
                return Conflict();
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            var otherUser = await _context.Users.FirstOrDefaultAsync(u => 
                (u.IIN == updatedUser.IIN || u.Email == updatedUser.Email) 
                && u.Id != id);

            if (otherUser is not null)
            {
                return Conflict();
            }

            user.Email = updatedUser.Email;
            user.Name = updatedUser.Name;
            user.DateOfBirth = updatedUser.DateOfBirth;
            user.IIN = updatedUser.IIN;

            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user is null)
            {
                return NotFound();
            }
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
