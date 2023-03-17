using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class MissionService : IMissionService
    {
        public readonly HvZDbContext _context;
        public MissionService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task DeleteMissionAsync(int gameId, int missionId)
        {
            var missionDomain = await _context.Missions.FindAsync(gameId, missionId);

            _context.Missions.Remove(missionDomain);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MissionDomain>> GetAllGameMissionsAsync(int gameId)
        {
            var missions = await _context.Missions.Where(m => m.GameId == gameId)
                .ToListAsync();

            return missions;
        }

        public async Task<MissionDomain> GetGameMissionAsync(int gameId, int missionId)
        {
            var mission = await _context.Missions.FindAsync(gameId, missionId);

            return mission;
        }

        public async Task<MissionDomain> PostMissionAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMissionAsync(MissionDomain mission, int gameId, int missionId)
        {
            throw new NotImplementedException();
        }
    }
}