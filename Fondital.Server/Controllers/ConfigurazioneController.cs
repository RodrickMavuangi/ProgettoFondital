using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("configurazioniControl")]
    [Authorize(Roles = "Direzione")]
    public class ConfigurazioneController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IConfigurazioneService _confService;

        public ConfigurazioneController(Serilog.ILogger logger, IConfigurazioneService confService)
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
            try
            {
                await _confService.UpdateValore(config);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Configurazione", config.Chiave);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Configurazione", config.Chiave, ex.Message);
                throw;
            }
        }
    }
}