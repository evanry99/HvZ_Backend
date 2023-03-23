﻿using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.GameDTO;
using HvZ.Model.DTO.KillDTO;
using HvZ.Model.DTO.PlayerDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GameDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;

        public GameDomainsController(IMapper mapper, IGameService gameService)
        {
            _mapper = mapper;
            _gameService = gameService;
        }

        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns>A list of games</returns>
        /// <response code="200"> Success. Returns a list of Games</response>
        /// <response code="500"> Internal error</response>
        // GET: api/GameDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameReadDTO>>> GetGame()
        {
            var gameModel = await _gameService.GetAllGamesAsync();
            var gameReadDTO = _mapper.Map<List<GameReadDTO>>(gameModel);
            return gameReadDTO;
        }


        /// <summary>
        /// Get a game by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific game</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> The game was not found</response>
        /// <response code="500"> Internal error</response>
        // GET: api/GameDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameReadDTO>> GetGameDomain(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {id}. The gameId must be greater than zero.");
            }
            var gameReadDTO = await _gameService.GetGameAsync(id);

            if (gameReadDTO == null)
            {
                return NotFound($"Game with id {id} does not exist");
            }

            return _mapper.Map<GameReadDTO>(gameReadDTO);
        }

        /// <summary>
        /// Update a game by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameDTO"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. Game updated</response>
        /// <response code="404"> The game was not found</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="500"> Internal error</response>
        // PUT: api/GameDomains/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDomain(GameEditDTO gameDTO, int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {id}. The gameId must be greater than zero.");
            }

            if (!_gameService.GameExists(id))
            {
                return NotFound($"Game with id {id} does not exist");
            }

            if (gameDTO.EndTime <= gameDTO.StartTime)
            {
                return BadRequest("Game start or end time is invalid");
            }

            var gameModel = _mapper.Map<GameDomain>(gameDTO);

            await _gameService.UpdateGameAsync(gameModel, id);

            return NoContent();
        }

        /// <summary>
        /// Add a new game
        /// </summary>
        /// <param name="gameDTO"></param>
        /// <returns></returns>
        /// <response code="201"> Game created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="500"> Internal error</response>
        // POST: api/GameDomains
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GameReadDTO>> PostGameDomain(GameCreateDTO gameDTO)
        {
            if (gameDTO.EndTime <= gameDTO.StartTime)
            {
                return BadRequest("Game start or end time is invalid");
            }

            var gameModel = _mapper.Map<GameDomain>(gameDTO);

            await _gameService.AddGameAsync(gameModel);

            return CreatedAtAction("GetGameDomain", new { id = gameModel.Id }, gameDTO);
        }


        /// <summary>
        /// Delete a game by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204"> Game deleted succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> Game not found</response>
        /// <response code="500"> Internal error</response>
        // DELETE: api/GameDomains/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDomain(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {id}. The gameId must be greater than zero.");
            }
            if (!_gameService.GameExists(id))
            {
                return NotFound($"Game with id {id} does not exist");
            }
            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }


    }
}
