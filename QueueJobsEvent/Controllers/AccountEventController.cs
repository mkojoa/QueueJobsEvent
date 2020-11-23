using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueJobsEvent.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountEventController : Controller
    {
        public AccountEventController()
        {

        }

        [HttpGet("")]
        public IActionResult TriggerExpensiveStuff()
        {
            return Ok();
        }
    }
}
