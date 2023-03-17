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
            if (await PlayerAlreadyJoinedGameAsync(gameId, playerId))
            {
                throw new Exception("Player has already joine the game.");
            }

            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if(!gameExists)
            {
                return null;
            }

            var squadToAdd = new SquadDomain
            {
                Name = squad.Name,
                IsHuman = squad.IsHuman,
                GameId = gameId,
            };
            _context.Squads.Add(squadToAdd);

            var suqadMemberToAdd = new SquadMemberDomain
            {
                Rank = "Squad Leader",
                GameId = gameId,
                SquadId = squadToAdd.Id,
                PlayerId = playerId

            };
            _context.SquadMembers.Add(suqadMemberToAdd);

            await _context.SaveChangesAsync();
            return squadToAdd;
        }

        public async Task DeleteSquadAsync(int gameId, int squadId)
        {
            if(!await SquadExistsAsync(squadId)) 
            {
                throw new Exception($"Squad with id {squadId} not found. ");
            }

            var squadMembers = await _context.SquadMembers.Where(sm => sm.GameId == gameId && sm.SquadId== squadId).ToListAsync();
            _context.SquadMembers.RemoveRange(squadMembers);

            var squad = await _context.Squads.FindAsync(gameId, squadId);
            _context.Squads.Remove(squad);
        }

        public async Task<IEnumerable<SquadDomain>> GetAllSquadsAsync(int gameId)
        {
            return await _context.Squads.ToListAsync();
        }

        public async Task<SquadDomain> GetSquadAsync(int gameId, int squadId)
        {
            return await _context.Squads.FindAsync(gameId, squadId);
        }

        public async Task<SquadMemberDomain> AddSquadMemberAsync(int gameId, int squadId, int playerId)
        {
            if (!await SquadExistsAsync(squadId))
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
            var squadMember = new SquadMemberDomain
            {
                GameId = gameId,
                SquadId = squadId,
                PlayerId = playerId,
                Rank = "Squad member" // Set default rank to squad member
            };

            await _context.SquadMembers.AddAsync(squadMember);
            await _context.SaveChangesAsync();

            return squadMember;

        }

        public async Task <SquadDomain> UpdateSquadAsync(int gameId, int squadId, SquadDomain squad)
        {
            if (!await SquadExistsAsync(squadId))
            {
                return null;
            }
            SquadDomain existingsSquad = await _context.Squads.FindAsync(gameId, squadId);
            existingsSquad.Name = squad.Name;
            //existingsSquad.GameId = gameId; should we update gameId?
            //existingsSquad.IsHuman= squad.IsHuman;

            await _context.SaveChangesAsync();
            return existingsSquad;
        }
        public async Task<bool> PlayerAlreadyJoinedGameAsync(int gameId, int playerId)
        {
            return await _context.SquadMembers.AnyAsync(sm => sm.GameId == gameId && sm.PlayerId == playerId);
        }

        public async Task<bool> SquadExistsAsync(int squadId)
        {
            return await _context.Squads.AnyAsync(s => s.Id == squadId);
        }

        public async Task<bool> IsHumanAsync(int playerId)
        {
            return await _context.Players
                .Where(p => p.Id == playerId)
                .Select(p => p.IsHuman)
                .FirstOrDefaultAsync();
        }
    }
}
