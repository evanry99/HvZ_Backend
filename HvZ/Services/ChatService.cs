using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class ChatService : IChatService
    {
        public readonly HvZDbContext _context;
        public ChatService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<ChatDomain> AddChatAsync(ChatDomain chat, int gameId)
        {
            chat.GameId = gameId;

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return chat;
        }

        public bool ChatExists(int chatId)
        {
            return _context.Chats.Any(g => g.Id == chatId);
        }

        public async Task DeleteChatAsync(int chatId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatDomain>> GetChatsAsync(int gameId)
        {
            var chats = await _context.Chats.Where(c => c.GameId == gameId).ToListAsync();

            return chats;
        }
    }
}