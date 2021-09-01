using Fondital.Data;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("tracesControl")]
    public class TraceController : ControllerBase
    {
        private readonly ILogger<TraceController> _logger;
        private readonly ITraceService _traceService;

        public TraceController(ILogger<TraceController> logger, ITraceService traceService)
        {
            _logger = logger;
            _traceService = traceService;
        }

        [HttpGet]
        public async Task<IEnumerable<Trace>> Get()
        {
            return await _traceService.GetAllTraces();
        }

        [HttpPost]
        public async Task<Trace> PostDummy(Trace trace)
        {
            try
            {
                return await _traceService.CreateTrace(trace);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
