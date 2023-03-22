using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class SquadCheckInService : ISquadCheckInService
    {
        public readonly HvZDbContext _context;
        public SquadCheckInService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to create a new squad check-in in a game.
        /// </summary>
        /// <param name="squadCheckIn"></param>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<SquadCheckInDomain> AddSquadCheckInAsync(SquadCheckInDomain squadCheckIn, int gameId, int squadId)
        {
            squadCheckIn.SquadId = squadId;
            squadCheckIn.GameId = gameId;

            _context.SquadCheckIns.Add(squadCheckIn);

            await _context.SaveChangesAsync();
            return squadCheckIn;
        }
        
        /// <summary>
        /// Method to get all squad check-ins in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SquadCheckInDomain>> GetSquadCheckInsAsync(int gameId, int squadId)
        {
            var squadCheckIns = await _context.SquadCheckIns
                .Where(s => s.SquadId == squadId)
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            return squadCheckIns;
        }

        /// <summary>
        /// Method to check if a squad exists.
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public bool SquadExists(int squadId)
        {
            return _context.Squads.Any(s => s.Id == squadId);
        }
    }
}
