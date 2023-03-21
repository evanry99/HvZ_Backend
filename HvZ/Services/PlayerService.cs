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
        
            player.BiteCode = GenerateBiteCode();
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

        /// <summary>
        /// Generates a random six-character string consisting of characters from the string "123456"
        /// Then check if a player is using the BiteCode, if a player are using it then it create a new BiteCode
        /// until a new unique BiteCode are generated
        /// </summary>
        /// <returns></returns>
        private string GenerateBiteCode()
        {
            const string chars = "123456";
            var random = new Random();
            var biteCode = new string (Enumerable.Repeat(chars,6).Select(s => s[random.Next(s.Length)]).ToArray());

            //Check if bitcode exsist
            while (_context.Players.Any(p => p.BiteCode == biteCode))
            {
                biteCode = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return biteCode;
        }
    }
}
