using AutoMapper;
using HvZ.Data;
using HvZ.Model.Domain;
using HvZ.Model.DTO.GameDTO;
using HvZ.Model.DTO.KillDTO;
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
        [HttpGet("{gameId}/mission")]
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
        [HttpPut("{gameId}/mission/{missionId}")]
        public async Task<IActionResult> PutMissionDomain(MissionEditDTO missionDTO, int gameId, int missionId)
        {
            if (gameId != missionDTO.GameId)
            {
                return BadRequest();
            }

            if (!_missionService.GameExists(gameId))
            {
                return NotFound();
            }

            var missionModel = _mapper.Map<MissionDomain>(missionDTO);

            await _missionService.UpdateMissionAsync(missionModel, gameId, missionId);

            return NoContent();
        }

        // POST: api/MissionDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{gameId}/mission")]
        public async Task<ActionResult<MissionReadDTO>> PostMissionDomain(MissionCreateDTO missionDTO, int gameId)
        {
          if (!_missionService.GameExists(gameId))
          {
              return NotFound($"Game with id {gameId} does not exist");
          }

            var missionDomain = _mapper.Map<MissionDomain>(missionDTO);

            await _missionService.PostMissionAsync(missionDomain, gameId);

            return CreatedAtAction("PostMissionDomain", new { id = missionDomain.Id }, missionDomain);
        }

        // DELETE: api/MissionDomains/5
        [HttpDelete("{gameId}/mission/{missionId}")]
        public async Task<IActionResult> DeleteMissionDomain(int gameId, int missionId)
        {
            if (!_missionService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            await _missionService.DeleteMissionAsync(gameId, missionId);

            return NoContent();
        }
    }
}
