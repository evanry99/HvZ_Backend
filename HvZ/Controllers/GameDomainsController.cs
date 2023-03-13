﻿using System;
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
using HvZ.Model.DTO.GameDTO;


namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;

        public GameDomainsController(IMapper mapper, IGameService gameService)
        {
            _mapper = mapper;
            _gameService = gameService;
        }

        // GET: api/GameDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameReadDTO>>> GetGames()
        {
            var gameModel = await _gameService.GetAllGamesAsync();
            var gameReadDTO = _mapper.Map<List<GameReadDTO>>(gameModel);
            return gameReadDTO;
        }

        // GET: api/GameDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameReadDTO>> GetGameDomain(int id)
        {
            var gameReadDTO = await _gameService.GetGameAsync(id);

            if (gameReadDTO == null)
            {
                return NotFound();
            }

            return _mapper.Map<GameReadDTO>(gameReadDTO);
        }



        // PUT: api/GameDomains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDomain(int id, GameEditDTO gameDTO)
        {
            if (id != gameDTO.Id)
            {
                return BadRequest();
            }

            if (!_gameService.GameExists(id))
            {
                return NotFound();
            }

            var gameModel = _mapper.Map<GameDomain>(gameDTO);
            await _gameService.UpdateGameAsync(gameModel);
            return NoContent();
        }

        // POST: api/GameDomains
        [HttpPost]
        public async Task<ActionResult<GameReadDTO>> PostGameDomain(GameCreateDTO gameDTO)
        {
            var gameModel = _mapper.Map<GameDomain>(gameDTO);
            await _gameService.AddGameAsync(gameModel);

            return CreatedAtAction("GetGameDomain", new { id = gameModel.Id }, gameDTO);
        }

        // DELETE: api/GameDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDomain(int id)
        {
            if (!_gameService.GameExists(id))
            {
                return NotFound();
            }
            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }

    }
}