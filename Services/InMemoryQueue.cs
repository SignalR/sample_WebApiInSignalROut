using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WebApiInSignalROut.Services
{
    public class InMemoryQueue
    {
        public InMemoryQueue()
        {
            CallerQueue = Channel.CreateUnbounded<Tuple<string, DateTime>>();
        }

        internal Channel<Tuple<string, DateTime>> CallerQueue { get; private set; }

        public async Task Enqueue(string groupName)
        {
            await CallerQueue.Writer.WriteAsync(
                new Tuple<string, DateTime>(groupName, DateTime.Now.AddSeconds(10))
            );
        }
    }
}