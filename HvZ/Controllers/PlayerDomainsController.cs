using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.PlayerDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PlayerDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;

        public PlayerDomainsController(IMapper mapper, IPlayerService playerService)
        {
            _mapper = mapper;
            _playerService = playerService;
        }

        /// <summary>
        /// Get all players in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Returns a list of players</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> Game not found</response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/player")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PlayerReadDTO>>> GetGamePlayers(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (!_playerService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }
            return _mapper.Map<List<PlayerReadDTO>>(await _playerService.GetAllGamePlayersAsync(gameId));
        }

        /// <summary>
        /// Get a player by game Id and player Id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific player</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Player not found</response>
        /// <response code="500"> Internal error</response>
        [Authorize]
        [HttpGet("{gameId}/player/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlayerReadDTO>> GetPlayerDomain(int gameId, int playerId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (playerId <= 0)
            {
                return BadRequest($"Invalid playerId parameter id {playerId}. The player must be greater than zero.");
            }

            if (!_playerService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!_playerService.PlayerExists(playerId))
            {
                return NotFound($"Player with id {playerId} does not exist");
            }

            var playerDomain = await _playerService.GetPlayerAsync(gameId, playerId);
            if(playerDomain == null)
            {
                return NotFound($"Player with id {playerId} does not exist in game {gameId}");
            }
            return _mapper.Map<PlayerReadDTO>(playerDomain);
        }

        /// <summary>
        /// Update a player by game Id and player Id
        /// </summary>
        /// <param name="playerDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. Player updated</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Player or Game not found</response>
        /// <response code="500"> Internal error</response>
        [Authorize]
        [HttpPut("{gameId}/player/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPlayerDomain(PlayerEditDTO playerDTO, int gameId, int playerId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (playerId <= 0)
            {
                return BadRequest($"Invalid playerId parameter id {playerId}. The gameId must be greater than zero.");
            }

            if (!_playerService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!_playerService.PlayerExists(playerId))
            {
                return NotFound($"Player with id {playerId} does not exist in game {gameId}");
            }

            var playerCheckDomain = await _playerService.GetPlayerAsync(gameId, playerId);
            if (playerCheckDomain == null)
            {
                return NotFound($"Player with id {playerId} does not exist in game {gameId}");
            }

            PlayerDomain playerDomain = _mapper.Map<PlayerDomain>(playerDTO);
            await _playerService.UpdatePlayerAsync(playerDomain, gameId, playerId);

            return NoContent();
        }

        /// <summary>
        /// Add a new player to a game
        /// </summary>
        /// <param name="playerDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="201"> Player created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found</response>
        /// <response code="500"> Internal error</response>
        [Authorize]
        [HttpPost("{gameId}/player")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlayerReadDTO>> PostPlayerDomain(PlayerCreateDTO playerDTO, int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (!_playerService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            PlayerDomain playerDomain = _mapper.Map<PlayerDomain>(playerDTO);
            await _playerService.AddPlayerAsync(playerDomain, gameId);

            return CreatedAtAction("PostPlayerDomain", new { id = playerDomain.Id }, playerDomain);
        }

        /// <summary>
        /// Delete a user by game Id and player Id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        /// <response code="204"> Player deleted succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Player or Game not found</response>
        /// <response code="500"> Internal error</response>
        [Authorize]
        [HttpDelete("{gameId}/player/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlayerDomain(int gameId, int playerId)
        {

            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (playerId <= 0)
            {
                return BadRequest($"Invalid playerId parameter id {playerId}. The gameId must be greater than zero.");
            }

            if (!_playerService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!_playerService.PlayerExists(playerId))
            {
                return NotFound($"Player with id {playerId} does not exist in game {gameId}");
            }

            var playerCheckDomain = await _playerService.GetPlayerAsync(gameId, playerId);
            if (playerCheckDomain == null)
            {
                return NotFound($"Player with id {playerId} does not exist in game {gameId}");
            }

            await _playerService.DeletePlayerAsync(gameId, playerId);

            return NoContent();
        }
    }
}