using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<PlayerDomain>> GetAllPlayersInGameAsync(int gameId);
        public Task<PlayerDomain> GetSpecificPlayerInGameAsync(int gameId, int playerId);
        public Task AddPlayerAsync(int gameId, PlayerDomain player);
        public Task UpdatePlayer(int gameId, int playerId, PlayerDomain player);
        public Task Deleteplayer(int gameId, int playerId);
    }
}