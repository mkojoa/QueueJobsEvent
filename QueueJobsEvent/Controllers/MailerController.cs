using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueJobsEvent.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MailerController : Controller
    {
        private IQueue _queue;

        public MailerController(IQueue queue)
        {
            _queue = queue; 
        }

    }
}
