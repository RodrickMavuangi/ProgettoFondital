using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("configurazioniControl")]
    [Authorize]
    public class ConfigurazioneController : ControllerBase
    {
        private readonly ILogger<ConfigurazioneController> _logger;
        private readonly IConfigurazioneService _confService;

        public ConfigurazioneController(ILogger<ConfigurazioneController> logger, IConfigurazioneService confService)
        {
            _logger = logger;
            _confService = confService;
        }

        [HttpGet]
        public async Task<IEnumerable<Configurazione>> Get()
        {
            return await _confService.GetAllConfigurazioni();
        }

        [HttpPost("update")]
        public async Task UpdateConfigurazione([FromBody] Configurazione config)
        {
            await _confService.UpdateValore(config);
        }
    }
}