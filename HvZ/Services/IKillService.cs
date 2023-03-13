using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IKillService
    {
        public Task<IEnumerable<KillDomain>> GetAllKillsAsync(int gameId);
        public Task<KillDomain> GetSpecificKillAsync(int gameId, int killId);
        public Task AddKillAsync(int gameId, KillDomain kill);
        public Task UpdateKillAsync(int gameId, KillDomain kill);
        public Task DeleteKillAsync(int gameId);
    }
}