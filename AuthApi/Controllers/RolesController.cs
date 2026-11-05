using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthUsersContext _context;

        public RolesController(AuthUsersContext context)
        {
            _context = context;
        }

        [HttpPost("NewRole")]

        public async Task<IActionResult> NewRole([FromBody] NewRolesDTO newroledto)
        {
            var roles = new Role
            {
                Id = Guid.NewGuid(),
                Name = newroledto.Name
            };
            if (roles is null)
            {
                return StatusCode(400, new { message = "NEMJÓ" });
            }

            await _context.Roles.AddAsync(roles);
            {
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "JÓ", result = roles });
            }

        }

        [HttpGet("allRoles")]
        public async Task<IActionResult> AllRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            if (roles is null)
            {
                return StatusCode(400, new { message = "NEMJÓ" });

            }
            return StatusCode(201, new { message = "JÓ", result = roles });
        }

        [HttpGet("AllRolesById")]

        public async Task<IActionResult> ByID([FromQuery] Guid id)
        {
            var roles = await _context.Roles.SingleOrDefaultAsync(u => u.Id == id);

            if (roles is null)
            {
                return StatusCode(404, new { message = "NINCS" });

            }
            else if (roles is not null)
            {
                return StatusCode(200, new { message = "JÓ", result = roles });
            }
            return StatusCode(400, new { message = "NEMJÓ" });
        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateUsers([FromQuery] Guid id, [FromBody] UpdateRolesDTO updaterolesdto)
        {
            var roles = await _context.Roles.FirstOrDefaultAsync(u => u.Id == id);

            if (roles is null)
            {
                return StatusCode(404, new { message = "NINCS" });

            }
            else if (roles is not null)
            {
                roles.Name = updaterolesdto.Name;
               

                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "FRISS", result = roles });
            }
            return StatusCode(400, new { message = "NEMJÓ" });
        }

        [HttpDelete("DeleteRoles")]

        public async Task<IActionResult> DeleteRoles([FromQuery] Guid id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles is null)
            {
                return StatusCode(404, new { message = "NINCS" });

            }
            else if (roles is not null)
            {

                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "TÖRÖLVE", result = roles });
            }
            return StatusCode(400, new { message = "NEMJÓ" });
        }

    }
}
