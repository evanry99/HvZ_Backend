using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ.Data;
using HvZ.Model.Domain;

namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public UserDomainsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/UserDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDomain>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDomain>> GetUserDomain(int id)
        {
            var userDomain = await _context.Users.FindAsync(id);

            if (userDomain == null)
            {
                return NotFound();
            }

            return userDomain;
        }

        // PUT: api/UserDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDomain(int id, UserDomain userDomain)
        {
            if (id != userDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDomainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDomain>> PostUserDomain(UserDomain userDomain)
        {
            _context.Users.Add(userDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDomain", new { id = userDomain.Id }, userDomain);
        }

        // DELETE: api/UserDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDomain(int id)
        {
            var userDomain = await _context.Users.FindAsync(id);
            if (userDomain == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDomainExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
