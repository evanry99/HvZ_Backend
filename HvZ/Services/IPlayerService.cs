using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<PlayerDomain>> GetAllGamePlayersAsync(int gameId);
        public Task<PlayerDomain> GetPlayerAsync(int gameId, int playerId);
        public Task<PlayerDomain> AddPlayerAsync(PlayerDomain player, int gameId);
        public Task UpdatePlayerAsync(PlayerDomain player, int gameId, int playerId);
        public Task DeletePlayerAsync(int gameId, int playerId);
        public bool PlayerExists(int playerId);
        public bool GameExists(int gameId);
    }
}