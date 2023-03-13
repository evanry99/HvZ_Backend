using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IKillService
    {
        public Task<IEnumerable<KillDomain>> GetAllKillsAsync();
        public Task<KillDomain> GetKillAsync(int killId);
        public Task<KillDomain> AddKillAsync(KillDomain kill);
        public Task UpdateKillAsync(KillDomain kill);
        public Task DeleteKillAsync(int killId);
        public bool KillExists(int killId);
    }
}