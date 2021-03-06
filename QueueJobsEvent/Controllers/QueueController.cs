﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueueJobsEvent.Invocables;

namespace QueueJobsEvent.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QueueController : Controller
    {
        private IQueue _queue;

        public QueueController(IQueue queue)
        {
            this._queue = queue;
        }

        [HttpGet("")]
        public IActionResult TriggerExpensiveWorkInvocables()
        {
            this._queue.QueueInvocable<DoExpensiveCalculationAndStore>();
            return Ok();
        }
    }
}
