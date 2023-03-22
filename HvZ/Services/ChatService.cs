using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.AspNetCore.Mvc;
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

        public async Task DeleteChatAsync(int gameId, int chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.GameId == gameId && c.Id == chatId);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatDomain>> GetGlobalChatsAsync(int gameId)
        {
            var chats = await _context.Chats
                .Where(c => c.GameId == gameId && c.IsZombieGlobal == true && c.IsHumanGlobal == true).ToListAsync();

            return chats;
        }

        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

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

        public async Task<IEnumerable<ChatDomain>> GetSquadChatsAsync(int gameId, int squadId)
        {
            return await _context.Chats.Where(c => c.SquadId == squadId).ToListAsync();
        }
    }
}