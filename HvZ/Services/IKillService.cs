using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IKillService
    {
        public Task<IEnumerable<KillDomain>> GetAllKillsAsync(int gameId);
        public Task<KillDomain> GetKillAsync(int gameId, int killId);
        public Task<KillDomain> AddKillAsync(KillDomain kill, int gameId);
        public Task UpdateKillAsync(KillDomain kill, int gameId, int killId);
        public Task DeleteKillAsync(int gameId, int killId);
        public bool KillExists(int killId);
        public bool GameExists(int gameId);
    }
}