﻿using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IMissionService
    {
        public Task<IEnumerable<MissionDomain>> GetAllGameMissionsAsync(int gameId);
        public Task<MissionDomain> GetGameMissionAsync(int gameId, int missionId);
        public Task<MissionDomain> PostMissionAsync(MissionDomain mission, int gameId);
        public Task UpdateMissionAsync(MissionDomain mission, int gameId, int missionId);
        public Task DeleteMissionAsync(int gameId, int missionId);
        public bool GameExists(int gameId);
        public bool MissionExists(int missionId);
    }
}