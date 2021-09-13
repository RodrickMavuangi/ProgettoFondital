using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateLavorazione([FromBody] Lavorazione lavorazione)
        {
            try
            {
                await _lavorazioneService.AddLavorazione(lavorazione);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.LogError($"{ex.Message} - INNER EXCEPTION: {ex.InnerException.Message}");
                else
                    _logger.LogError(ex.Message);

                List<Lavorazione> lista = (List<Lavorazione>)await _lavorazioneService.GetAllLavorazioni();

                if (lista.Any(x => x.NomeItaliano == lavorazione.NomeItaliano))
                    return BadRequest("ErroreNomeItaliano");
                else if (lista.Any(x => x.NomeRusso == lavorazione.NomeRusso))
                    return BadRequest("ErroreNomeRusso");
                else
                    return BadRequest("ErroreGenerico");
            }
        }
    }
}
