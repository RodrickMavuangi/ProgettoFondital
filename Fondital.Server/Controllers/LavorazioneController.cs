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
    [Route("lavorazioniControl")]
    public class LavorazioneController : ControllerBase
    {
        private readonly ILogger<LavorazioneController> _logger;
        private readonly ILavorazioneService _lavorazioneService;
        private readonly IConfiguration _configuration;

        public LavorazioneController(ILogger<LavorazioneController> logger, FonditalDbContext db, ILavorazioneService lavorazioneService, IConfiguration configuration)
        {
            _logger = logger;
            _lavorazioneService = lavorazioneService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Lavorazione>> Get([FromQuery] bool? isEnabled)
        {
            return await _lavorazioneService.GetAllLavorazioni(isEnabled);
        }

        [HttpPost("update/{lavorazioneId}")]
        public async Task UpdateLavorazione([FromBody] Lavorazione lavorazione, int lavorazioneId)
        {
            await _lavorazioneService.UpdateLavorazione(lavorazioneId, lavorazione);
        }

        [HttpPost]
        public async Task CreateLavorazione([FromBody] Lavorazione lavorazione)
        {
            await _lavorazioneService.AddLavorazione(lavorazione);
        }
    }
}
