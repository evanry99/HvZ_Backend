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
    [Route("api/player")]
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
        /// Get all players
        /// </summary>
        /// <returns></returns>
        // GET: api/PlayerDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerReadDTO>>> GetPlayers()
        {
            return _mapper.Map<List<PlayerReadDTO>>(await _playerService.GetAllPlayersAsync());
        }

        /// <summary>
        /// Get a player by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/PlayerDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerReadDTO>> GetPlayerDomain(int id)
        {
            var playerDomain = await _playerService.GetPlayerAsync(id);

            if (playerDomain == null)
            {
                return NotFound();
            }

            return _mapper.Map<PlayerReadDTO>(playerDomain);
        }

        /// <summary>
        /// Update a player by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerDTO"></param>
        /// <returns></returns>
        // PUT: api/PlayerDomains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerDomain(int id, PlayerEditDTO playerDTO)
        {
            if (id != playerDTO.Id)
            {
                return BadRequest();
            }

            if (!_playerService.PlayerExists(id))
            {
                return NotFound();
            }

            PlayerDomain playerDomain = _mapper.Map<PlayerDomain>(playerDTO);
            await _playerService.UpdatePlayerAsync(playerDomain);

            return NoContent();
        }

        /// <summary>
        /// Add a new player
        /// </summary>
        /// <param name="playerDTO"></param>
        /// <returns></returns>
        // POST: api/PlayerDomains
        [HttpPost]
        public async Task<ActionResult<PlayerReadDTO>> PostPlayerDomain(PlayerCreateDTO playerDTO)
        {
            PlayerDomain playerDomain = _mapper.Map<PlayerDomain>(playerDTO);
            await _playerService.AddPlayerAsync(playerDomain);

            return CreatedAtAction("GetPlayerDomain", new { id = playerDomain.Id }, playerDomain);
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/PlayerDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerDomain(int id)
        {
            if (!_playerService.PlayerExists(id))
            {
                return NotFound();
            }

            await _playerService.DeletePlayerAsync(id);

            return NoContent();
        }
    }
}
