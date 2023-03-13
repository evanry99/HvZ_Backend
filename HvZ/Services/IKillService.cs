using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IKillService
    {
        public Task<IEnumerable<KillDomain>> GetAllKillsAsync(int gameId);
        public Task<KillDomain> GetKillAsync(int gameId, int killId);
        public Task<KillService> AddKillAsync(int gameId, KillDomain kill);
        public Task UpdateKillAsync(int gameId, KillDomain kill);
        public Task DeleteKillAsync(int gameId);
        public bool KillExists(int gameId, int killId);
    }
}