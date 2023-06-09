﻿using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IChatService
    {
        public Task<IEnumerable<ChatDomain>> GetGameChatsAsync(int gameId);
        public Task<ChatDomain> AddChatAsync(ChatDomain chat, int gameId);
        public Task DeleteChatAsync(int gameId, int chatId);
        public bool ChatExists(int chatId);
        public bool GameExists(int gameId);
        public bool PlayerExists(int gameId);
        public bool SquadExists(int squadId);
    }
}