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
    public class SquadDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public SquadDomainsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/SquadDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadDomain>>> GetSquads()
        {
          if (_context.Squads == null)
          {
              return NotFound();
          }
            return await _context.Squads.ToListAsync();
        }

        // GET: api/SquadDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadDomain>> GetSquadDomain(int id)
        {
          if (_context.Squads == null)
          {
              return NotFound();
          }
            var squadDomain = await _context.Squads.FindAsync(id);

            if (squadDomain == null)
            {
                return NotFound();
            }

            return squadDomain;
        }

        // PUT: api/SquadDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquadDomain(int id, SquadDomain squadDomain)
        {
            if (id != squadDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(squadDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SquadDomainExists(id))
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

        // POST: api/SquadDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SquadDomain>> PostSquadDomain(SquadDomain squadDomain)
        {
          if (_context.Squads == null)
          {
              return Problem("Entity set 'HvZDbContext.Squads'  is null.");
          }
            _context.Squads.Add(squadDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSquadDomain", new { id = squadDomain.Id }, squadDomain);
        }

        // DELETE: api/SquadDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquadDomain(int id)
        {
            if (_context.Squads == null)
            {
                return NotFound();
            }
            var squadDomain = await _context.Squads.FindAsync(id);
            if (squadDomain == null)
            {
                return NotFound();
            }

            _context.Squads.Remove(squadDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SquadDomainExists(int id)
        {
            return (_context.Squads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
