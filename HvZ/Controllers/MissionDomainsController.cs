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
        private readonly IMapper _mapper;
        private readonly IMissionService _missionService;

        public MissionDomainsController(IMapper mapper, IMissionService missionService)
        {
            _mapper = mapper;
            _missionService = missionService;
        }

        /// <summary>
        /// Get all missions in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a specific mission in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edit a mission in a game
        /// </summary>
        /// <param name="missionDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a new mission record in a game
        /// </summary>
        /// <param name="missionDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a mission in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
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
