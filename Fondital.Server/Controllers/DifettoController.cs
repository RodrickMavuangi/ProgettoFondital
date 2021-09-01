using Fondital.Data;
using Fondital.Services;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("difettiControl")]
    public class DifettoController : ControllerBase
    {
        private readonly ILogger<DifettoController> _logger;
        private readonly IDifettoService _difettoService;
        private readonly IConfiguration _configuration;

        public DifettoController(ILogger<DifettoController> logger, FonditalDbContext db, IDifettoService difettoService, IConfiguration configuration)
        {
            _logger = logger;
            _difettoService = difettoService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Difetto>> Get([FromQuery] bool? isEnabled)
        {
            return await _difettoService.GetAllDifetti(isEnabled);
        }

        //[HttpGet("{page}")]
        //public async Task<IEnumerable<Difetto>> GetByPage(int page, [FromQuery]bool? isEnabled)
        //{
        //    int pageSize = Convert.ToInt32(_configuration["PageSize"]);
        //    return await _difettoService.GetDifettiByPage(page, pageSize, isEnabled);
        //}

        [HttpPost("update/{difettoId}")]
        public async Task UpdateDifetto([FromBody] Difetto difetto, int difettoId)
        {
            await _difettoService.UpdateDifetto(difettoId, difetto);
        }
    }
}
