using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IGameService
    {
        public Task <IEnumerable<GameDomain>> GetAllGamesAsync();
        public Task <GameDomain> GetGameAsync(int gameId);
        public Task <GameDomain> AddGameAsync(GameDomain game);
        public Task UpdateGameAsync(GameDomain game, int gameId);
        public Task DeleteGameAsync(int gameId);
        public bool GameExists(int gameId);
    }
}
