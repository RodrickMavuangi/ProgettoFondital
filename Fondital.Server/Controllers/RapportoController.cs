using AutoMapper;
using Fondital.Shared.Dto;
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
    [Route("rapportiControl")]
    [Authorize]
    public class RapportoController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRapportoService _rapportoService;
        private readonly IMapper _mapper;

        public RapportoController(Serilog.ILogger logger, IRapportoService rapportoService, IMapper mapper)
        {
            _logger = logger;
            _rapportoService = rapportoService;
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
        public async Task UpdateRapporto([FromBody] RapportoDto rapportoDto, int rapportoId)
        {
            Rapporto rapportoToUpdate = _mapper.Map<Rapporto>(rapportoDto);

            try
            {
                await _rapportoService.UpdateRapporto(rapportoId, rapportoToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Rapporto", rapportoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Rapporto", rapportoId, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task CreateRapporto([FromBody] RapportoDto rapportoDto)
        {
            try
            {
                Rapporto newRapporto = _mapper.Map<Rapporto>(rapportoDto);
                int rapportoId = await _rapportoService.AddRapporto(newRapporto);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "Rapporto", rapportoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "Rapporto", ex.Message);
                throw;
            }
        }
    }
}