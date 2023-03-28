using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace HvZ.Services
{
    public class PlayerService : IPlayerService
    {
        public readonly HvZDbContext _context;

        public PlayerService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to get all players in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerDomain>> GetAllGamePlayersAsync(int gameId)
        {
            var players = await _context.Players.Where(p => p.GameId == gameId).ToListAsync();

            foreach (var player in players)
            {
                string decodedBiteCode = HttpUtility.HtmlDecode(player.BiteCode);
                player.BiteCode = decodedBiteCode;
            }
            return players;
        }

        /// <summary>
        /// Method to get a specific player in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<PlayerDomain> GetPlayerAsync(int gameId, int playerId)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.GameId == gameId && p.Id == playerId);
        }

        /// <summary>
        /// Method to add a new player to a game.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<PlayerDomain> AddPlayerAsync(PlayerDomain player, int gameId)
        {
            string encodeBiteCode = HttpUtility.HtmlEncode(player.BiteCode);
            player.BiteCode = encodeBiteCode;
            player.GameId = gameId;
            player.BiteCode = GenerateBiteCode();
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        /// <summary>
        /// Method to update a player in a game.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task UpdatePlayerAsync(PlayerDomain player, int gameId, int playerId)
        {
            player.Id = playerId;
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to delete a player in a game. First it manually deletes all records which has a player as parent or grandparent,
        /// then deletes the player itself.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task DeletePlayerAsync(int gameId, int playerId)
        {
            var squadMember = await _context.SquadMembers.Where(sm => sm.PlayerId == playerId).FirstOrDefaultAsync();
            if (squadMember != null)
            {
                var squadCheckIns = await _context.SquadCheckIns.Where(sc => sc.SquadMemberId == squadMember.Id).ToListAsync();
                _context.SquadCheckIns.RemoveRange(squadCheckIns);
                _context.SquadMembers.Remove(squadMember);
            }

            var kills = await _context.Kills.Where(k => k.KillerId == playerId).ToListAsync();
            _context.Kills.RemoveRange(kills);
            var kills2 = await _context.Kills.Where(k => k.VictimId == playerId).ToListAsync();
            _context.Kills.RemoveRange(kills2);
            var chats = await _context.Chats.Where(c => c.PlayerId == playerId).ToListAsync();
            _context.RemoveRange(chats);

            var player = await _context.Players.FirstOrDefaultAsync(p => p.GameId == gameId && p.Id == playerId);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to check if player exists.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public bool PlayerExists(int playerId)
        {
            return _context.Players.Any(p => p.Id == playerId);
        }

        /// <summary>
        /// Method to check if a game exists.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public bool GameExists(int gameId)
        {
            return  _context.Games.Any(g => g.Id == gameId);
        }

        /// <summary>
        /// Generates a random six-character string consisting of characters from the string "123456"
        /// Then check if a player is using the BiteCode, if a player are using it then it create a new BiteCode
        /// until a new unique BiteCode are generated
        /// </summary>
        /// <returns></returns>
        private string GenerateBiteCode()
        {
            const string chars = "123456";
            var random = new Random();
            var biteCode = new string (Enumerable.Repeat(chars,6).Select(s => s[random.Next(s.Length)]).ToArray());

            //Check if bitcode exists
            while (_context.Players.Any(p => p.BiteCode == biteCode))
            {
                biteCode = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return biteCode;
        }
    }
}
