using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<PlayerDomain>> GetAllPlayersAsync();
        public Task<PlayerDomain> GetPlayerAsync(int playerId);
        public Task<PlayerDomain> AddPlayerAsync(PlayerDomain player);
        public Task UpdatePlayer(PlayerDomain player);
        public Task Deleteplayer(int playerId);
        public bool PlayerExists(int playerId);
    }
}