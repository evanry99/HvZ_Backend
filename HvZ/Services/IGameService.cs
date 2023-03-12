using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IGameService
    {
        public Task<IEnumerable<GameDomain>> GetAllGamesAsync();
        public Task <GameDomain> GetGameAsync(int Id);
        public Task<GameDomain> AddGameAsync(GameDomain game);
        public Task<GameDomain> UpdateGameAsync(GameDomain game);
        public Task<GameDomain> DeleteGameAsync(int id);
        public bool GameExists(int id);

    }
}
