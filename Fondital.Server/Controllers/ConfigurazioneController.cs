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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("configurazioniControl")]
    public class ConfigurazioneController : ControllerBase
    {
        private readonly ILogger<ConfigurazioneController> _logger;
        private readonly FonditalDbContext _db;
        private readonly IConfigurazioneService _confService;

        public ConfigurazioneController(ILogger<ConfigurazioneController> logger, FonditalDbContext db, IConfigurazioneService confService)
        {
            _logger = logger;
            _db = db;
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
