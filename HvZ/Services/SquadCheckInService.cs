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

        public async Task<SquadCheckInDomain> AddSquadCheckInAsync(SquadCheckInDomain squadCheckIn, int gameId, int squadId)
        {
            squadCheckIn.SquadId = squadId;
            squadCheckIn.GameId = gameId;

            _context.SquadCheckIns.Add(squadCheckIn);

            await _context.SaveChangesAsync();
            return squadCheckIn;
        }
        
        public async Task<IEnumerable<SquadCheckInDomain>> GetSquadCheckInsAsync(int gameId, int squadId)
        {
            var squadCheckIns = await _context.SquadCheckIns
                .Where(s => s.SquadId == squadId)
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            return squadCheckIns;
        }

        public bool SquadExists(int squadId)
        {
            return _context.Squads.Any(s => s.Id == squadId);
        }
    }
}
