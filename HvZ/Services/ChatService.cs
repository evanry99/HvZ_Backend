using HvZ.Data;
using HvZ.Model.Domain;

namespace HvZ.Services
{
    public class ChatService : IChatService
    {
        public readonly HvZDbContext _context;
        public ChatService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<ChatDomain> AddChatAsync(ChatDomain chat, int gameId)
        {
            throw new NotImplementedException();
        }

        public bool ChatExists(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatDomain>> GetChatsAsync(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
