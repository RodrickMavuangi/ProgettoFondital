using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("lavorazioniControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class LavorazioneController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly ILavorazioneService _lavorazioneService;
        private readonly IMapper _mapper;

        public LavorazioneController(Serilog.ILogger logger, ILavorazioneService lavorazioneService, IMapper mapper)
        {
            _logger = logger;
            _lavorazioneService = lavorazioneService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LavorazioneDto>> Get([FromQuery] bool? isEnabled)
        {
            return _mapper.Map<IEnumerable<LavorazioneDto>>(await _lavorazioneService.GetAllLavorazioni(isEnabled));
        }

        [HttpPost("update/{lavorazioneId}")]
        [Authorize(Roles = "Direzione")]
        public async Task UpdateLavorazione([FromBody] LavorazioneDto lavorazioneDto, int lavorazioneId)
        {
            Lavorazione lavorazioneToUpdate = _mapper.Map<Lavorazione>(lavorazioneDto);

            try
            {
                await _lavorazioneService.UpdateLavorazione(lavorazioneId, lavorazioneToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Lavorazione", lavorazioneId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Lavorazione", lavorazioneId, ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> CreateLavorazione([FromBody] LavorazioneDto lavorazioneDto)
        {
            Lavorazione lavorazione = _mapper.Map<Lavorazione>(lavorazioneDto);
            try
            {
                int lavorazioneId = await _lavorazioneService.AddLavorazione(lavorazione);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "Lavorazione", lavorazioneId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage} - INNER EXCEPTION: {InnerException}", "CREATE", "Lavorazione", ex.Message, ex.InnerException.Message);
                else
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "Lavorazione", ex.Message);

                List<LavorazioneDto> lista = (List<LavorazioneDto>)await _lavorazioneService.GetAllLavorazioni();

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