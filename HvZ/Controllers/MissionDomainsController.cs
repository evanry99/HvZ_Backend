using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.MissionDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <response code="200"> Success. Returns a list of Games</response>
        /// <response code="404"> The game was not found</response>
        /// <response code="500"> Internal error</response>
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
        /// <response code="200"> Success. Return a specific mission in a game</response>
        /// <response code="404"> Game or mission not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/mission/{missionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <response code="204"> Update success. Mission updated</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> The game or mission was not found</response>
        /// <response code="500"> Internal error</response>
        [HttpPut("{gameId}/mission/{missionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutMissionDomain(MissionEditDTO missionDTO, int gameId, int missionId)
        {
            if (!_missionService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (missionDTO.EndTime <= missionDTO.StartTime)
            {
                return BadRequest("Mission start or end time is invalid");
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
        /// <response code="201"> Mission created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/mission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MissionReadDTO>> PostMissionDomain(MissionCreateDTO missionDTO, int gameId)
        {
            if (!_missionService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (missionDTO.EndTime <= missionDTO.StartTime)
            {
                return BadRequest("Mission start or end time is invalid");
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
        /// <response code="204"> Mission deleted succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> Game or mission not found</response>
        /// <response code="500"> Internal error</response>
        [HttpDelete("{gameId}/mission/{missionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
