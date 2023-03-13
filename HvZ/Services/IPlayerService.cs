using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<PlayerDomain>> GetAllPlayersInGameAsync(int gameId);
        public Task<PlayerDomain> GetSpecificPlayerInGameAsync(int gameId, int playerId);
    }
}