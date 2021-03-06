using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiInSignalROut.Hubs;

namespace WebApiInSignalROut.Services
{
    public class SampleLongRunningService : BackgroundService
    {
        public InMemoryQueue InMemoryQueue { get; }
        public IHubContext<SampleHub> SampleHubContext { get; }
        public ILogger<SampleLongRunningService> Logger { get; }

        public SampleLongRunningService(
            InMemoryQueue inMemoryQueue,
            IHubContext<SampleHub> sampleHubContext,
            ILogger<SampleLongRunningService> logger)
        {
            InMemoryQueue = inMemoryQueue;
            SampleHubContext = sampleHubContext;
            Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var item = await InMemoryQueue.CallerQueue.Reader.ReadAsync(stoppingToken);

                Logger.LogInformation($"Entry {item.Item1} queued at {item.Item2}. Notifying sender.");
                await SampleHubContext.Clients.Group(item.Item1).SendAsync("serverSideProcessComplete", item.Item1);

                await Task.Delay(10000);
            }
        }
    }
}
