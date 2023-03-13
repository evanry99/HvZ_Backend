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
using HvZ.Model.DTO.GameDTO;
using HvZ.Model.DTO.UserDTO;

namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;

        public GameDomainsController(HvZDbContext context, IMapper mapper, IGameService gameService)
        {
            _context = context;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameDomain(int id, GameDomain gameDomain)
        {
            if (id != gameDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameDomainExists(id))
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

        // POST: api/GameDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDomain>> PostGameDomain(GameDomain gameDomain)
        {
            _context.Games.Add(gameDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameDomain", new { id = gameDomain.Id }, gameDomain);
        }

        // DELETE: api/GameDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameDomain(int id)
        {
            var gameDomain = await _context.Games.FindAsync(id);
            if (gameDomain == null)
            {
                return NotFound();
            }

            _context.Games.Remove(gameDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameDomainExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
