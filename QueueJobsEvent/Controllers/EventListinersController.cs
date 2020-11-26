using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QueueJobsEvent.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueJobsEvent.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventListinersController : Controller
    {
        private IQueue _queue;

        public EventListinersController(IQueue queue)
        {
            _queue = queue;
        }

        [HttpGet("")]
        public IActionResult TriggerEventListiners()
        {
            // Trigger Event
            var accountCreated = new AccountCreated("Message from Controller to be proccessed");

            _queue.QueueBroadcast(accountCreated);

            return Ok();
        }
    }
}
