using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace HvZ.Services
{
    public class KillService : IKillService
    {
        public readonly HvZDbContext _context;

        public KillService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add a new kill in a game.
        /// </summary>
        /// <param name="kill"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<KillDomain> AddKillAsync(KillDomain kill, int gameId)
        {
            string encodeStory = HttpUtility.HtmlEncode(kill.Story);
            kill.Story = encodeStory;

            kill.GameId = gameId;
            _context.Kills.Add(kill);
            await _context.SaveChangesAsync();
            return kill;
        }

        /// <summary>
        /// Method to delete a kill in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        public async Task DeleteKillAsync(int gameId, int killId)
        {
            var kill = await _context.Kills.FirstOrDefaultAsync(k => k.GameId == gameId && k.Id == killId);
            _context.Kills.Remove(kill);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get all kills in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<KillDomain>> GetAllKillsAsync(int gameId)
        {
            var kills = await _context.Kills.Where(k => k.GameId == gameId).ToListAsync();
            
            foreach (var kill in kills)
            {
                string decodedStory = HttpUtility.HtmlDecode(kill.Story);
                kill.Story = decodedStory;
            }

            return kills; 
        }

        /// <summary>
        /// Method to get a specific kill in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        public async Task<KillDomain> GetKillAsync(int gameId, int killId)
        {   
            return await _context.Kills.FirstOrDefaultAsync(k => k.GameId == gameId && k.Id == killId);
        }   

        /// <summary>
        /// Method to check if a kill exists.
        /// </summary>
        /// <param name="killId"></param>
        /// <returns></returns>
        public bool KillExists(int killId)
        {
            return _context.Kills.Any(k => k.Id == killId);
        }

        public bool GameExists (int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }

        /// <summary>
        /// Method to update a kill in a game.
        /// </summary>
        /// <param name="kill"></param>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        public async Task UpdateKillAsync(KillDomain kill, int gameId, int killId)
        {
            _context.Entry(kill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
