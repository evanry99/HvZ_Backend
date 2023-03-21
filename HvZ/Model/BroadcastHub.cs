using HvZ.Model.DTO.ChatDTO;
using Microsoft.AspNetCore.SignalR;

namespace HvZ.Model
{
    public class BroadcastHub: Hub
    {
        public Task SendChat(ChatCreateDTO chat)
        {
            return Clients.All.SendAsync("chat", chat);
        }
    }
}
