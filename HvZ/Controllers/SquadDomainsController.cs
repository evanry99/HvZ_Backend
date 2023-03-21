using AutoMapper;
using HvZ.Data;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadCheckInDTO;
using HvZ.Model.DTO.SquadDTO;
using HvZ.Model.DTO.SquadMemberDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("game/")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquadDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISquadCheckInService _squadCheckInService;
        private readonly ISquadService _squadService;

        public SquadDomainsController(HvZDbContext context, IMapper mapper, ISquadCheckInService squadCheckInService, ISquadService squadService)
        {
            _context = context;
            _mapper = mapper;
            _squadCheckInService = squadCheckInService;
            _squadService = squadService;
        }

        /// <summary>
        /// Get all squad by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        // GET: api/SquadDomains
        [HttpGet("{gameId}/squad")]
        public async Task<ActionResult<IEnumerable<SquadReadDTO>>> GetSquads(int gameId)
        {
            var squadModel = await _squadService.GetAllSquadsAsync(gameId);
            var gameReadDTO = _mapper.Map<List<SquadReadDTO>>(squadModel);
            return Ok(gameReadDTO);

        }


        /// <summary>
        /// Get a specific squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        // GET: api/SquadDomains/5
        [HttpGet("{gameId}/squad/{squadId}")]
        public async Task<ActionResult<SquadReadDTO>> GetSquadDomain(int gameId, int squadId)
        {
            var squadReadDTO = await _squadService.GetSquadAsync(gameId, squadId);

            if (squadReadDTO == null)
            {
                return NotFound();
            }
            return _mapper.Map<SquadReadDTO>(squadReadDTO);
        }

        /// <summary>
        /// Update a squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <param name="squadDTO"></param>
        /// <returns></returns>
        // PUT: api/SquadDomains/5
        [HttpPut(("{gameId}/squads/{squadId}"))]
        public async Task<IActionResult> UpdateSquad(int gameId, int squadId, SquadEditDTO squadDTO)
        {
            if (!await _squadService.SquadExistsAsync(gameId, squadId))
            {
                return NotFound();
            }
            var squadModel = _mapper.Map<SquadDomain>(squadDTO);
            await _squadService.UpdateSquadAsync(gameId,squadId,squadModel);
            return NoContent();

        }

        /// <summary>
        /// Create a new squad in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadDTO"></param>
        /// <returns></returns>
        // POST: api/SquadDomains
        [HttpPost("{gameId}/squad")]
        public async Task<ActionResult<SquadReadDTO>> PostSquad(int gameId,SquadCreateDTO squadDTO)
        {
            var squadDomain = _mapper.Map<SquadDomain>(squadDTO);
            var addedSquad = await _squadService.AddSquadAsync(gameId, squadDomain, squadDTO.SquadMember.PlayerId);
            if (addedSquad == null)
            {
                return NotFound();
            }
            var squadReadDTO = _mapper.Map<SquadReadDTO>(addedSquad);
            return CreatedAtAction("PostSquad", new { gameId = addedSquad.GameId, squadId = addedSquad.Id }, squadReadDTO);
        }

        /// <summary>
        /// Add a squad member to the specific squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <param name="squadMemberDTO"></param>
        /// <returns></returns>
        // Post add squad member
        [HttpPost("{gameId}/squad/{squadId}/join")]
        public async Task<ActionResult<SquadMemberReadDTO>> PostSquadMember(int gameId, int squadId, SquadMemberCreateDTO squadMemberDTO)
        {
            var squadMemberDomain = _mapper.Map<SquadMemberDomain>(squadMemberDTO);
            var addedSquadMember = await _squadService.AddSquadMemberAsync(gameId, squadId, squadMemberDomain, squadMemberDTO.PlayerId);
            var squadMemberReadDTO = _mapper.Map<SquadMemberReadDTO>(addedSquadMember);
            return CreatedAtAction("PostSquadMember", new { gameId = addedSquadMember.GameId, squadId = addedSquadMember.SquadId, squadMemberId = addedSquadMember.Id }, squadMemberReadDTO);
        }

        /// <summary>
        /// Delete a squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        // DELETE: api/SquadDomains/5
        [HttpDelete("{gameId}/squad/{squadId}")]
        public async Task<IActionResult> DeleteSquadDomain(int gameId, int squadId)
        {
            await _squadService.DeleteSquadAsync(gameId, squadId);
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
