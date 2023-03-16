using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface ISquadService
    {
        public Task <IEnumerable<SquadDomain>> GetAllSquadsAsync(int gameId);
        public Task <SquadDomain> GetSquadAsync(int gameId, int squadId);
        public Task <SquadDomain> AddSquadAsync(int gameId,SquadDomain squad, int playerId);
        public Task <SquadMemberDomain> AddSquadMemberAsync(int gameId, int squadId, int playerId);
        public Task <SquadDomain> UpdateSquadAsync(int gameId, int squadId, SquadDomain squad);
        public Task DeleteSquadAsync(int gameId, int squadId);
        public Task<bool> SquadExistsAsync(int squadId);
        public Task<bool> PlayerAlreadyJoinedGameAsync(int gameId, int playerId);
        public Task<bool> IsHumanAsync(int playerId);


    }
}
