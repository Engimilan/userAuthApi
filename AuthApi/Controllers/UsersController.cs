using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthUsersContext _context;

        public UsersController(AuthUsersContext context)
        {
            _context = context;
        }

        [HttpPost("NewUser")]

        public async Task<IActionResult> NewUser([FromBody] NewUserDTO newuserdto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = newuserdto.Name,
                Email = newuserdto.Email,
                Password = newuserdto.Password,
            };
            if (user is null)
            {
                return StatusCode(400, new { message = "NEMJÓ" });
            }

            await _context.Users.AddAsync(user);
            {
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "JÓ", result = user });
            }

        }

        [HttpGet("allUser")]
        public async Task<IActionResult> AllUser()
        {
            var users = await _context.Users.ToListAsync();

            if (users is null)
            {
                return StatusCode(400, new { message = "NEMJÓ" });

            }
            return StatusCode(201, new { message = "JÓ", result = users });
        }

        [HttpGet("AllUsersById")]

        public async Task<IActionResult> ByID([FromQuery]Guid id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(u=>u.Id==id);

            if (users is null)
            {
                return StatusCode(404, new { message = "NINCS" });

            }
            else if (users is not null) {
                return StatusCode(200, new { message = "JÓ", result = users });
            }
            return StatusCode(400, new { message = "NEMJÓ" });
        }

    }
}
