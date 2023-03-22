using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IGameService
    {
        public Task <IEnumerable<GameDomain>> GetAllGamesAsync();
        public Task <GameDomain> GetGameAsync(int id);
        public Task<IEnumerable<KillDomain>> GetGameKillsAsync(int id);
        public Task <GameDomain> AddGameAsync(GameDomain game);
        public Task UpdateGameAsync(GameDomain game, int id);
        public Task DeleteGameAsync(int id);
        public bool GameExists(int id);

    }
}
