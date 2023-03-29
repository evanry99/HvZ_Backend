using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System;

namespace HvZ.Services
{
    public class GameService : IGameService
    {
        public readonly HvZDbContext _context;

        public GameService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to create a new game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public async Task<GameDomain> AddGameAsync(GameDomain game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        /// <summary>
        /// Method to delete a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task DeleteGameAsync(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to check if game exists.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }

        /// <summary>
        /// Method to get all games.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GameDomain>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        /// <summary>
        /// Method to get a specific game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<GameDomain> GetGameAsync(int gameId)
        {
            return await _context.Games.FindAsync(gameId);
        }

        /// <summary>
        /// Method to update a game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task UpdateGameAsync(GameDomain game, int gameId)
        {
            game.Id = gameId;
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
