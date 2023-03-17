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
    public class SquadCheckInDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public SquadCheckInDomainsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/SquadCheckInDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadCheckInDomain>>> GetSquadCheckIns()
        {
          if (_context.SquadCheckIns == null)
          {
              return NotFound();
          }
            return await _context.SquadCheckIns.ToListAsync();
        }

        // GET: api/SquadCheckInDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadCheckInDomain>> GetSquadCheckInDomain(int id)
        {
          if (_context.SquadCheckIns == null)
          {
              return NotFound();
          }
            var squadCheckInDomain = await _context.SquadCheckIns.FindAsync(id);

            if (squadCheckInDomain == null)
            {
                return NotFound();
            }

            return squadCheckInDomain;
        }

        // PUT: api/SquadCheckInDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquadCheckInDomain(int id, SquadCheckInDomain squadCheckInDomain)
        {
            if (id != squadCheckInDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(squadCheckInDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SquadCheckInDomainExists(id))
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

        // POST: api/SquadCheckInDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SquadCheckInDomain>> PostSquadCheckInDomain(SquadCheckInDomain squadCheckInDomain)
        {
          if (_context.SquadCheckIns == null)
          {
              return Problem("Entity set 'HvZDbContext.SquadCheckIns'  is null.");
          }
            _context.SquadCheckIns.Add(squadCheckInDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSquadCheckInDomain", new { id = squadCheckInDomain.Id }, squadCheckInDomain);
        }

        // DELETE: api/SquadCheckInDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquadCheckInDomain(int id)
        {
            if (_context.SquadCheckIns == null)
            {
                return NotFound();
            }
            var squadCheckInDomain = await _context.SquadCheckIns.FindAsync(id);
            if (squadCheckInDomain == null)
            {
                return NotFound();
            }

            _context.SquadCheckIns.Remove(squadCheckInDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SquadCheckInDomainExists(int id)
        {
            return (_context.SquadCheckIns?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
