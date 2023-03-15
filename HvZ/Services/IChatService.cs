using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IChatService
    {
        public Task<IEnumerable<ChatDomain>> GetChatsAsync(int gameId);
        public Task<ChatDomain> AddChatAsync(ChatDomain chat, int gameId);
        public Task DeleteChatAsync(int chatId);
        public bool ChatExists(int chatId);
    }
}
