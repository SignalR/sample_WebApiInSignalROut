using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApiInSignalROut.Hubs;
using WebApiInSignalROut.Services;

namespace WebApiInSignalROut.Controllers
{
    public class SampleController : ControllerBase
    {
        public SampleController(InMemoryQueue queue, IHubContext<SampleHub> sampleHub)
        {
            InMemoryQueue = queue;
            SampleHub = sampleHub;
        }

        public InMemoryQueue InMemoryQueue { get; }
        public IHubContext<SampleHub> SampleHub { get; }

        [HttpGet]
        [Route("sample/{caller}")]
        public async Task<ActionResult> Get([FromRoute] string caller)
        {
            await Task.Delay(5000);
            InMemoryQueue.Enqueue(caller);
            return Ok();
        }
    }
}