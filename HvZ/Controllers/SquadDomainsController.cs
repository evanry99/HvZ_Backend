using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ.Data;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadCheckInDTO;
using HvZ.Services;
using AutoMapper;

namespace HvZ.Controllers
{
    [Route("api/squad")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquadDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISquadCheckInService _squadCheckInService;

        public SquadDomainsController(HvZDbContext context, IMapper mapper, ISquadCheckInService squadCheckInService)
        {
            _context = context;
            _mapper = mapper;
            _squadCheckInService = squadCheckInService;
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

        /// <summary>
        /// Get all squad check-ins by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        [HttpGet("{gameId}/squad/{squadId}/check-in")]
        public async Task<ActionResult<IEnumerable<SquadCheckInReadDTO>>> GetSquadCheckIns(int gameId, int squadId)
        {
            if (!_squadCheckInService.SquadExists(squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist");
            }

            var squadCheckInModel = await _squadCheckInService.GetSquadCheckInsAsync(gameId, squadId);

            return Ok(_mapper.Map<List<SquadCheckInReadDTO>>(squadCheckInModel));
        }

        /// <summary>
        /// Post a new squad check-in by gameId and squadId
        /// </summary>
        /// <param name="squadCheckInDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        [HttpPost("{gameId}/squad/{squadId}/check-in")]
        public async Task<ActionResult<SquadCheckInReadDTO>> PostSquadCheckIn(SquadCheckInCreateDTO squadCheckInDTO, int gameId, int squadId)
        {
            if (!_squadCheckInService.SquadExists(squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist");
            }

            SquadCheckInDomain squadCheckInDomain = _mapper.Map<SquadCheckInDomain>(squadCheckInDTO);

            await _squadCheckInService.AddSquadCheckInAsync(squadCheckInDomain, gameId, squadId);

            return CreatedAtAction("PostSquadCheckIn", new { id = squadCheckInDomain.Id }, squadCheckInDomain);
        }

        private bool SquadDomainExists(int id)
        {
            return (_context.Squads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
