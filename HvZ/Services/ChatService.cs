using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace HvZ.Services
{
    public class ChatService : IChatService
    {
        public readonly HvZDbContext _context;
        public ChatService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Async method to add a new chat to DB. GameId gets automatically set.
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<ChatDomain> AddChatAsync(ChatDomain chat, int gameId)
        {
            chat.GameId = gameId;

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return chat;
        }

        /// <summary>
        /// Method to check if chat exists in DB.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public bool ChatExists(int chatId)
        {
            return _context.Chats.Any(g => g.Id == chatId);
        }

        /// <summary>
        /// Method to delete chat record from DB.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task DeleteChatAsync(int gameId, int chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.GameId == gameId && c.Id == chatId);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get chat messages for the global chat. Returns chat messages for both factions.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ChatDomain>> GetGlobalChatsAsync(int gameId)
        {
            var chats = await _context.Chats
                .Where(c => c.GameId == gameId && c.IsZombieGlobal == true && c.IsHumanGlobal == true).ToListAsync();

            return chats;
        }

        /// <summary>
        /// Method to check if game exists in DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

        /// <summary>
        /// Method to get faction chat messages. Takes input player Id and checks 
        /// if player is human then returns appropiate faction chat messages.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<ChatDomain>> GetFactionChatsAsync(int gameId, int playerId)
        {
            var playerModel = await _context.Players.FindAsync(playerId);

            if (playerModel == null)
            {
                throw new ArgumentException($"Player with id {playerId} could not be found");
            }

            if (playerModel.IsHuman == true)
            {
                return await _context.Chats
                    .Where(c => c.GameId == gameId && c.IsHumanGlobal == true && c.IsZombieGlobal == false).ToListAsync();
            }
            else
            {
                return await _context.Chats
                    .Where(c => c.GameId == gameId && c.IsHumanGlobal == false && c.IsZombieGlobal == true).ToListAsync();
            }
        }

        /// <summary>
        /// Method to get squad chat messages with a specific squad Id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ChatDomain>> GetSquadChatsAsync(int gameId, int squadId)
        {
            return await _context.Chats.Where(c => c.SquadId == squadId).ToListAsync();
        }

        /// <summary>
        /// Method to get all chat messages in a game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ChatDomain>> GetGameChatsAsync(int gameId)
        {
            return await _context.Chats.Where(c => c.GameId == gameId).ToListAsync();
        }
    }
}