using Fondital.Data;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
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
        private readonly FonditalDbContext _db;

        public TraceController(ILogger<TraceController> logger, FonditalDbContext db)
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

                return trace.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
