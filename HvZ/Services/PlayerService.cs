using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class PlayerService : IPlayerService
    {

        public readonly HvZDbContext _context;

        public PlayerService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayerDomain>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<PlayerDomain> GetPlayerAsync(int playerId)
        {
            return await _context.Players.FindAsync(playerId);
        }

        public async Task<PlayerDomain> AddPlayerAsync(PlayerDomain player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task UpdatePlayerAsync(PlayerDomain player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int playerId)
        {
            var player = await _context.Players.FindAsync(playerId);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public bool PlayerExists(int playerId)
        {
            return _context.Players.Any(p => p.Id == playerId);
        }
    }
}
