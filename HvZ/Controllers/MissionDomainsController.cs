using AutoMapper;
using HvZ.Data;
using HvZ.Model.Domain;
using HvZ.Model.DTO.MissionDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Controllers
{
    [Route("api/mission")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class MissionDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMissionService _missionService;

        public MissionDomainsController(HvZDbContext context, IMapper mapper, IMissionService missionService)
        {
            _context = context;
            _mapper = mapper;
            _missionService = missionService;
        }

        // GET: api/MissionDomains
        [HttpGet("{gameId}")]
        public async Task<ActionResult<IEnumerable<MissionReadDTO>>> GetAllGameMissions(int gameId)
        {
            if (!_missionService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var missionModel = await _missionService.GetAllGameMissionsAsync(gameId);

            return _mapper.Map<List<MissionReadDTO>>(missionModel);
        }

        // GET: api/MissionDomains/5
        [HttpGet("{gameId}/mission/{missionId}")]
        public async Task<ActionResult<MissionReadDTO>> GetMissionDomain(int gameId, int missionId)
        {
            if (!_missionService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var missionModel = await _missionService.GetGameMissionAsync(gameId, missionId);

            if (missionModel == null)
            {
                return NotFound();
            }

            return _mapper.Map<MissionReadDTO>(missionModel);
        }

        // PUT: api/MissionDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMissionDomain(int id, MissionDomain missionDomain)
        {
            if (id != missionDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(missionDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionDomainExists(id))
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

        // POST: api/MissionDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MissionDomain>> PostMissionDomain(MissionDomain missionDomain)
        {
          if (_context.Missions == null)
          {
              return Problem("Entity set 'HvZDbContext.Missions'  is null.");
          }
            _context.Missions.Add(missionDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMissionDomain", new { id = missionDomain.Id }, missionDomain);
        }

        // DELETE: api/MissionDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMissionDomain(int id)
        {
            if (_context.Missions == null)
            {
                return NotFound();
            }
            var missionDomain = await _context.Missions.FindAsync(id);
            if (missionDomain == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(missionDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MissionDomainExists(int id)
        {
            return (_context.Missions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
