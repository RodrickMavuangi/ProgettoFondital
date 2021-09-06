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
    [Route("vociCostoControl")]
    public class VoceCostoController : ControllerBase
    {
        private readonly ILogger<VoceCostoController> _logger;
        private readonly IVoceCostoService _voceCostoService;
        private readonly IConfiguration _configuration;

        public VoceCostoController(ILogger<VoceCostoController> logger, FonditalDbContext db, IVoceCostoService voceCostoService, IConfiguration configuration)
        {
            _logger = logger;
            _voceCostoService = voceCostoService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<VoceCosto>> Get([FromQuery] bool? isEnabled)
        {
            return await _voceCostoService.GetAllVociCosto(isEnabled);
        }

        [HttpPost("update/{voceCostoId}")]
        public async Task UpdateVoceCosto([FromBody] VoceCosto voceCosto, int voceCostoId)
        {
            await _voceCostoService.UpdateVoceCosto(voceCostoId, voceCosto);
        }

        [HttpPost]
        public async Task CreateVoceCosto([FromBody] VoceCosto voceCosto)
        {
            await _voceCostoService.AddVoceCosto(voceCosto);
        }
    }
}
