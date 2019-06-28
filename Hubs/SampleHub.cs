using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using WebApiInSignalROut.Services;

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