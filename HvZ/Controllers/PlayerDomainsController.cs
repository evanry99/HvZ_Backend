using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ.Data;
using HvZ.Model.Domain;
using AutoMapper;
using HvZ.Services;
using HvZ.Model.DTO.PlayerDTO;

namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;

        public PlayerDomainsController(HvZDbContext context, IMapper mapper, IPlayerService playerService)
        {
            _context = context;
            _mapper = mapper;
            _playerService = playerService;
        }

        // GET: api/PlayerDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerReadDTO>>> GetPlayers()
        {
            return _mapper.Map<List<PlayerReadDTO>>(await _context.Players.ToListAsync());
        }

        // GET: api/PlayerDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDomain>> GetPlayerDomain(int id)
        {
            var playerDomain = await _context.Players.FindAsync(id);

            if (playerDomain == null)
            {
                return NotFound();
            }

            return playerDomain;
        }

        // PUT: api/PlayerDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerDomain(int id, PlayerDomain playerDomain)
        {
            if (id != playerDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerDomainExists(id))
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

        // POST: api/PlayerDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerDomain>> PostPlayerDomain(PlayerDomain playerDomain)
        {
            _context.Players.Add(playerDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerDomain", new { id = playerDomain.Id }, playerDomain);
        }

        // DELETE: api/PlayerDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerDomain(int id)
        {
            var playerDomain = await _context.Players.FindAsync(id);
            if (playerDomain == null)
            {
                return NotFound();
            }

            _context.Players.Remove(playerDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerDomainExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
