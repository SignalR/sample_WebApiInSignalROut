using System;
using System.Collections.Generic;

namespace WebApiInSignalROut.Services
{
    public class InMemoryQueue
    {
        public InMemoryQueue()
        {
            CallerQueue = new List<Tuple<string, DateTime>>();
        }

        internal List<Tuple<string, DateTime>> CallerQueue { get; private set; }

        public void Enqueue(string groupName)
        {
            CallerQueue.Add(new Tuple<string, DateTime>(groupName, DateTime.Now.AddSeconds(10)));
        }
    }
}