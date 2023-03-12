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
    public class GameDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public GameDomainsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/GameDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDomain>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/GameDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDomain>> GetGameDomain(int id)
        {
            var gameDomain = await _context.Games.FindAsync(id);

            if (gameDomain == null)
            {
                return NotFound();
            }

            return gameDomain;
        }

        // PUT: api/GameDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDomain(int id, GameDomain gameDomain)
        {
            if (id != gameDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameDomainExists(id))
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

        // POST: api/GameDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDomain>> PostGameDomain(GameDomain gameDomain)
        {
            _context.Games.Add(gameDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameDomain", new { id = gameDomain.Id }, gameDomain);
        }

        // DELETE: api/GameDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDomain(int id)
        {
            var gameDomain = await _context.Games.FindAsync(id);
            if (gameDomain == null)
            {
                return NotFound();
            }

            _context.Games.Remove(gameDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameDomainExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
