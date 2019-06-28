using Microsoft.AspNetCore.SignalR;

namespace WebApiInSignalROut.Hubs
{
    public class SampleHub : Hub
    {
        public void StartMonitoring(string username)
        {
            base.Groups.AddToGroupAsync(base.Context.ConnectionId, username);
        }
    }
}