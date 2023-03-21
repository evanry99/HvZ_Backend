using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class SquadService : ISquadService
    {
        public readonly HvZDbContext _context;

        public SquadService(HvZDbContext context)
        {
            _context = context;
        }
        public async Task<SquadDomain> AddSquadAsync(int gameId, SquadDomain squad, int playerId)
        {
            // Check if the player is already in the game
            if (await PlayerAlreadyJoinedGameAsync(gameId, playerId))
            {
                throw new Exception("Player has already joined the game.");
            }

            // Check if the game exists
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExists)
            {
                return null;
            }

            // Check if the squad type matches the player's species
            if (squad.IsHuman != await IsHumanAsync(playerId))
            {
                throw new Exception("Player cannot join squad due to species mismatch.");
            }

            squad.GameId = gameId;
            SquadMemberDomain squadMember= new SquadMemberDomain() {PlayerId = playerId, GameId = gameId, Rank="Squad Leader"};
            squad.SquadMembers = new List<SquadMemberDomain>();
            squad.SquadMembers.Add(squadMember);

            _context.Squads.Add(squad);
            await _context.SaveChangesAsync();

            return squad;
        }

        public async Task DeleteSquadAsync(int gameId, int squadId)
        {
            var squad = await _context.Squads.FindAsync(squadId);

            if (squad == null || squad.GameId != gameId)
            {
                throw new Exception($"Squad with id {squadId} not found in game {gameId}.");
            }

            var squadMembers = await _context.SquadMembers.Where(sm => sm.GameId == gameId && sm.SquadId == squadId).ToListAsync();
            _context.SquadMembers.RemoveRange(squadMembers);

            _context.Squads.Remove(squad);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SquadDomain>> GetAllSquadsAsync(int gameId)
        {
            return await _context.Squads.Where(s => s.GameId == gameId).ToListAsync();
        }

        public async Task<SquadDomain> GetSquadAsync(int gameId, int squadId)
        {
            return await _context.Squads.FirstOrDefaultAsync(s => s.GameId == gameId && s.Id == squadId);
        }

        public async Task<SquadMemberDomain> AddSquadMemberAsync(int gameId, int squadId, SquadMemberDomain squadMember, int playerId)
        {
            if (!await SquadExistsAsync(gameId, squadId))
            {
                throw new Exception("Squad does not exist or is invalid.");
            }

            var squad = await _context.Squads.FindAsync(squadId);

            if (squad.IsHuman != await IsHumanAsync(playerId))
            {
                throw new Exception("Player cannot join squad due to species mismatch.");
            }

            if (await PlayerAlreadyJoinedGameAsync(gameId, playerId))
            {
                throw new Exception("Player has already joined a squad in this game.");
            }

            squadMember.GameId = gameId;
            squadMember.SquadId = squadId;
            squadMember.PlayerId = playerId;
            squadMember.Rank = "Squad Member";

            await _context.SquadMembers.AddAsync(squadMember);
            await _context.SaveChangesAsync();

            return squadMember;
        }


        public async Task <SquadDomain> UpdateSquadAsync(int gameId,int squadId, SquadDomain squad)
        {
            if (!await SquadExistsAsync(gameId, squadId))
            {
                return null;
            }
            SquadDomain existingsSquad = await _context.Squads.FindAsync(squadId);
            existingsSquad.Name = squad.Name;
            //existingsSquad.GameId = gameId; should we update gameId?
            existingsSquad.IsHuman= squad.IsHuman;

            await _context.SaveChangesAsync();
            return existingsSquad;
        }
        public async Task<bool> PlayerAlreadyJoinedGameAsync(int gameId, int playerId)
        {
            return await _context.SquadMembers.AnyAsync(sm => sm.GameId == gameId && sm.PlayerId == playerId);
        }

        public async Task<bool> SquadExistsAsync(int gameId, int squadId)
        {
            return await _context.Squads.AnyAsync(s => s.GameId == gameId && s.Id == squadId);
        }

        public async Task<bool> IsHumanAsync(int playerId)
        {
            return await _context.Players
                .Where(p => p.Id == playerId)
                .Select(p => p.IsHuman)
                .FirstOrDefaultAsync();
        }

        public async Task<SquadMemberDomain> GetSquadMemberAsync(int gameId, int playerId)
        {
            return await _context.SquadMembers.FirstOrDefaultAsync(sm => sm.GameId == gameId && sm.PlayerId == playerId);
        }
    }
}
