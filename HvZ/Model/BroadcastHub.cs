using HvZ.Services;
using Microsoft.AspNetCore.SignalR;

namespace HvZ.Model
{
    public class BroadcastHub: Hub<IHubClient>
    {
        public string getConnectionId() => Context.ConnectionId;
    }
}
