using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IHubClient
    {
        Task BroadcastMessage();
        Task BroadcastNotification(ChatDomain chat);
    }
}
