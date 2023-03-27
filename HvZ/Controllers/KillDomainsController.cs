using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.KillDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [Tags("Kill")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class KillDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IKillService _killService;

        public KillDomainsController(IMapper mapper, IKillService killService)
        {
            _mapper = mapper;
            _killService = killService;
        }

        /// <summary>
        /// Get all kills in a game by gameId
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of all kills </response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found </response> 
        /// <response code="500"> Internal error </response>
        [HttpGet("{gameId}/kill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<KillReadDTO>>> GetKills(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (!_killService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist ");
            }

            return _mapper.Map<List<KillReadDTO>>(await _killService.GetAllKillsAsync(gameId));
        }

        /// <summary>
        /// Get a kill by game Id and kill Id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific kill </response>
        /// <response code="400"> Bad request </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game/kill not found </response> 
        /// <response code="500"> Internal error </response>
        [HttpGet("{gameId}/kill/{killId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KillReadDTO>> GetKillDomain(int gameId, int killId)
        
        {
            if(gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if(killId <= 0)
            {
                return BadRequest($"Invalid killId parameter id {killId}. The killId must be greater than zero");
            }

            if (!_killService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist ");
            }

            if (!_killService.KillExists(killId))
            {
                return NotFound($"Kill with id {killId} does not exist ");
            }

            var killDomain = await _killService.GetKillAsync(gameId, killId);

            if (killDomain == null)
            {
                return NotFound($"Kill with id {killId} does not exist in game {gameId}");
            }

            return _mapper.Map<KillReadDTO>(killDomain);
        }

        /// <summary>
        /// Update a kill by id
        /// </summary>
        /// <param name="killDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. Kill updated  </response>
        /// <response code="400"> Bad request </response> 
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game/kill not found </response> 
        /// <response code="500"> Internal error </response>
        [HttpPut("{gameId}/kill/{killId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutKillDomain(KillEditDTO killDTO, int gameId, int killId)
        {
            if(gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (killId <= 0)
            {
                return BadRequest($"Invalid kill id {killId}. The killId must be greater than zero.");
            }

            if (!_killService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exsist");
            }

            if(!_killService.KillExists(killId))
            {
                return NotFound($"Kill with id {killId} does not exist");
            }


            KillDomain killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.UpdateKillAsync(killDomain, gameId, killId);

            return NoContent();
        }

        /// <summary>
        /// Add a new kill
        /// </summary>
        /// <param name="killDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="201"> Kill created successfully </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found </response>
        /// <response code="500"> Internal error </response>
        [HttpPost("{gameId}/kill")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KillReadDTO>> PostKillDomain(KillCreateDTO killDTO, int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (!_killService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exsist");
            }

            var killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.AddKillAsync(killDomain, gameId);

            return CreatedAtAction("PostKillDomain", new { id = killDomain.Id }, killDomain);
        }

        /// <summary>
        /// Delete a kill by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="204"> Kill deleted successfully </response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Kill not found </response>
        /// <response code="500"> Internal error </response>
        [HttpDelete("{gameId}/kill/{killId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteKillDomain(int gameId, int killId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (killId <= 0)
            {
                return BadRequest($"Invalid kill id {killId}. The killId must be greater than zero.");
            }

            if (!_killService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exsist");
            }

            if (!_killService.KillExists(killId))
            {
                return NotFound($"Kill with id {killId} does not exist");
            }

            var killCheckDomain = await _killService.GetKillAsync(gameId, killId);

            if (killCheckDomain == null)
            {
                return NotFound($"Kill with id {killId} does not exist in game {gameId}");
            }

            await _killService.DeleteKillAsync(gameId, killId);

            return NoContent();
        }
    }
}
