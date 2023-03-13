using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class GameService : IGameService
    {

        public readonly HvZDbContext _context;

        public GameService(HvZDbContext context)
        {
            _context = context;
        }
        public async Task<GameDomain> AddGameAsync(GameDomain game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

        public async Task<IEnumerable<GameDomain>> GetAllGamesAsync()
        {
            return await _context.Games.Include(g =>g.Players).Include(g => g.Kills).ToListAsync();
        }

        public async Task<GameDomain> GetGameAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task UpdateGameAsync(GameDomain game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
