using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface ISquadService
    {
        public Task <IEnumerable<SquadDomain>> GetAllSquadsAsync(int gameId);
        public Task <SquadDomain> GetSquadAsync(int gameId, int squadId);
        public Task <SquadDomain> AddSquadAsync(int gameId,SquadDomain squad, int playerId);
        public Task <SquadMemberDomain> JoinSquadAsync(int gameId, int squadId, SquadMemberDomain squadMember, int playerId);
        public Task UpdateSquadAsync(int gameId, int squadId, SquadDomain squad);
        public Task DeleteSquadAsync(int gameId, int squadId);
        public bool SquadExistsAsync(int squadId);


    }
}
