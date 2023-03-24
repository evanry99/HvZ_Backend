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

        /// <summary>
        /// Method to delete a mission in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public async Task DeleteMissionAsync(int gameId, int missionId)
        {
            var missionDomain = await _context.Missions.FirstOrDefaultAsync(m => m.GameId == gameId && m.Id == missionId);

            _context.Missions.Remove(missionDomain);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get all missions in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MissionDomain>> GetAllGameMissionsAsync(int gameId)
        {
            var missions = await _context.Missions.Where(m => m.GameId == gameId)
                .ToListAsync();

            return missions;
        }

        /// <summary>
        /// Method to get a specific mission in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public async Task<MissionDomain> GetGameMissionAsync(int gameId, int missionId)
        {
            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.GameId == gameId && m.Id == missionId);

            return mission;
        }

        /// <summary>
        /// Method to create a new mission in a game.
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<MissionDomain> PostMissionAsync(MissionDomain mission, int gameId)
        {
            mission.GameId = gameId;

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        /// <summary>
        /// Method to update a mission in a game.
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="gameId"></param>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public async Task UpdateMissionAsync(MissionDomain mission, int gameId, int missionId)
        {
            mission.GameId = gameId;
            mission.Id = missionId;

            _context.Entry(mission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to check if a game exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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