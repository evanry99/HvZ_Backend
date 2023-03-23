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
            var missionDomain = await _context.Missions.FirstOrDefaultAsync(m => m.GameId == gameId && m.Id == missionId);

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
            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.GameId == gameId && m.Id == missionId);

            return mission;
        }

        public async Task<MissionDomain> PostMissionAsync(MissionDomain mission, int gameId)
        {
            mission.GameId = gameId;

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async Task UpdateMissionAsync(MissionDomain mission, int gameId, int missionId)
        {
            mission.GameId = gameId;
            mission.Id = missionId;

            _context.Entry(mission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

        public bool MissionExists(int missionId)
        {
            return _context.Missions.Any(m => m.Id == missionId);
        }
    }
}