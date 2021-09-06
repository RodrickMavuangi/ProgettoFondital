using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        [HttpPost("update/{difettoId}")]
        public async Task UpdateDifetto([FromBody] Difetto difetto, int difettoId)
        {
            await _difettoService.UpdateDifetto(difettoId, difetto);
        }

        [HttpPost]
        public async Task CreateDifetto([FromBody] Difetto difetto)
        {
            await _difettoService.AddDifetto(difetto);
        }
    }
}
