using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<PlayerDomain>> GetAllPlayersAsync();
        public Task<PlayerDomain> GetPlayerAsync(int playerId);
        public Task<PlayerDomain> AddPlayerAsync(PlayerDomain player);
        public Task UpdatePlayerAsync(PlayerDomain player);
        public Task DeletePlayerAsync(int playerId);
        public bool PlayerExists(int playerId);
    }
}