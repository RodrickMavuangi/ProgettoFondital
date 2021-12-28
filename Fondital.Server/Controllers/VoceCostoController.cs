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
    [Route("vociCostoControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class VoceCostoController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IVoceCostoService _voceCostoService;
        private readonly IMapper _mapper;

        public VoceCostoController(Serilog.ILogger logger, IVoceCostoService voceCostoService, IMapper mapper)
        {
            _logger = logger;
            _voceCostoService = voceCostoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VoceCostoDto>> Get([FromQuery] bool? isEnabled)
        {
            return _mapper.Map<IEnumerable<VoceCostoDto>>(await _voceCostoService.GetAllVociCosto(isEnabled));
        }

        [HttpPost("update/{voceCostoId}")]
        [Authorize(Roles = "Direzione")]
        public async Task UpdateVoceCosto([FromBody] VoceCostoDto voceCostoDto, int voceCostoId)
        {
            VoceCosto voceCostoToUpdate = _mapper.Map<VoceCosto>(voceCostoDto);

            try
            {
                await _voceCostoService.UpdateVoceCosto(voceCostoId, voceCostoToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "VoceCosto", voceCostoId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "UPDATE", "VoceCosto", voceCostoId);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Direzione")]
        public async Task CreateVoceCosto([FromBody] VoceCostoDto voceCostoDto)
        {
            VoceCosto voceCosto = _mapper.Map<VoceCosto>(voceCostoDto);

            try
            {
                int voceCostoId = await _voceCostoService.AddVoceCosto(voceCosto);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "VoceCosto", voceCostoId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object}", "CREATE", "VoceCosto");
                throw;
            }
        }
    }
}