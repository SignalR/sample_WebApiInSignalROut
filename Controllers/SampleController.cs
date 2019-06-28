using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApiInSignalROut.Services;

namespace WebApiInSignalROut.Controllers
{
    public class SampleController : ControllerBase
    {
        public SampleController(InMemoryQueue queue)
        {
            InMemoryQueue = queue;
        }

        public InMemoryQueue InMemoryQueue { get; }

        [HttpGet]
        [Route("sample/{caller}")]
        public ActionResult Get([FromRoute] string caller)
        {
            InMemoryQueue.Enqueue(caller);
            return Ok();
        }
    }
}