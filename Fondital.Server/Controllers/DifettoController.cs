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
    [Route("difettiControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class DifettoController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IDifettoService _difettoService;
        private readonly IMapper _mapper;

        public DifettoController(Serilog.ILogger logger, IDifettoService difettoService, IMapper mapper)
        {
            _logger = logger;
            _difettoService = difettoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DifettoDto>> Get([FromQuery] bool? isEnabled)
        {
            return _mapper.Map<IEnumerable<DifettoDto>>(await _difettoService.GetAllDifetti(isEnabled));
        }

        [HttpPost("update/{difettoId}")]
        [Authorize(Roles = "Direzione")]
        public async Task UpdateDifetto([FromBody] DifettoDto difettoDto, int difettoId)
        {
            Difetto difettoToUpdate = _mapper.Map<Difetto>(difettoDto);

            try
            {
                await _difettoService.UpdateDifetto(difettoId, difettoToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Difetto", difettoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Difetto", difettoId, ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> CreateDifetto([FromBody] DifettoDto difettoDto)
        {
            Difetto difetto = _mapper.Map<Difetto>(difettoDto);
            try
            {
                int difettoId = await _difettoService.AddDifetto(difetto);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "Difetto", difettoId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage} - INNER EXCEPTION: {InnerException}", "CREATE", "Difetto", ex.Message, ex.InnerException.Message);
                else
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "Difetto", ex.Message);

                List<Difetto> lista = (List<Difetto>)await _difettoService.GetAllDifetti();

                if (lista.Any(x => x.NomeItaliano == difetto.NomeItaliano))
                    return BadRequest("ErroreNomeItaliano");
                else if (lista.Any(x => x.NomeRusso == difetto.NomeRusso))
                    return BadRequest("ErroreNomeRusso");
                else
                    return BadRequest("ErroreGenerico");
            }
        }
    }
}