using HvZ.Data;
using HvZ.Model.Domain;
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
        /// Method to check if game exists in DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

        /// <summary>
        /// Method to check if player exists in DB.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public bool PlayerExists(int playerId) 
        {
            return _context.Players.Any(p => p.Id == playerId);
        }

        /// <summary>
        /// Method to check if squad exists in DB.
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public bool SquadExists(int squadId)
        {
            return _context.Squads.Any(s => s.Id == squadId);
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