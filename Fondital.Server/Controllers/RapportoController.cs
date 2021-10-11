using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
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
    [Route("rapportiControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class RapportoController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRapportoService _rapportoService;
        private readonly IUtenteService _utService;
        private readonly IMapper _mapper;

        public RapportoController(Serilog.ILogger logger, IRapportoService rapportoService, IUtenteService utService, IMapper mapper)
        {
            _logger = logger;
            _rapportoService = rapportoService;
            _utService = utService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RapportoDto>> Get()
        {
            return _mapper.Map<IEnumerable<RapportoDto>>(await _rapportoService.GetAllRapporti());
        }

        [HttpGet("{id}")]
        public async Task<RapportoDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<RapportoDto>(await _rapportoService.GetRapportoById(id));
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "Rapporto", id, ex.Message);
                throw;
            }
        }

        [HttpPost("update/{rapportoId}")]
        public async Task<IActionResult> UpdateRapporto([FromBody] RapportoDto rapportoDto, int rapportoId)
        {
            Rapporto rapportoToUpdate = _mapper.Map<Rapporto>(rapportoDto);

            try
            {
                Utente UpdatingUser = await _utService.GetUtenteByUsername(this.HttpContext.User.Identity.Name);
                await _rapportoService.UpdateRapporto(rapportoId, rapportoToUpdate, UpdatingUser);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Rapporto", rapportoId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Rapporto", rapportoId, ex.Message);
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRapporto([FromBody] RapportoDto rapportoDto)
        {
            Rapporto newRapporto = _mapper.Map<Rapporto>(rapportoDto);

            try
            {
                Utente CreatingUser = await _utService.GetUtenteByUsername(this.HttpContext.User.Identity.Name);
                int rapportoId = await _rapportoService.AddRapporto(newRapporto, CreatingUser);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "Rapporto", rapportoId);

                return Ok(rapportoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "Rapporto", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}