using Fondital.Shared;
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
    [Route("traces")]
    public class TraceController : ControllerBase
    {
        private readonly ILogger<TraceController> _logger;
        private readonly FonditalDBContext _db;

        public TraceController(ILogger<TraceController> logger, FonditalDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trace>>> Get()
        {
            return await _db.Traces.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostDummy(Trace trace)
        {
            try
            {
                await _db.Traces.AddAsync(trace);
                await _db.SaveChangesAsync();

                return trace.id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
