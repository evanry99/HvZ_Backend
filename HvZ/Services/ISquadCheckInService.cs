using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface ISquadCheckInService
    {
        public Task<IEnumerable<SquadCheckInDomain>> GetSquadCheckInsAsync(int gameId, int squadId);
        public Task<SquadCheckInDomain> AddSquadCheckInAsync(SquadCheckInDomain squadCheckIn, int gameId, int squadId);
        public bool SquadExists(int squadId);
    }
}