using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Route("difettiControl")]
    [Authorize]
    public class DifettoController : ControllerBase
    {
        private readonly ILogger<DifettoController> _logger;
        private readonly IDifettoService _difettoService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DifettoController(ILogger<DifettoController> logger, IDifettoService difettoService, IConfiguration configuration, IMapper mapper)
        {
            _logger = logger;
            _difettoService = difettoService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DifettoDto>> Get([FromQuery] bool? isEnabled)
        {
            return _mapper.Map<IEnumerable<DifettoDto>>(await _difettoService.GetAllDifetti(isEnabled));
        }

        [HttpPost("update/{difettoId}")]
        public async Task UpdateDifetto([FromBody] DifettoDto difettoDto, int difettoId)
        {
            Difetto difettoToUpdate = _mapper.Map<Difetto>(difettoDto);
            await _difettoService.UpdateDifetto(difettoId, difettoToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDifetto([FromBody] DifettoDto difettoDto)
        {
            Difetto difetto = _mapper.Map<Difetto>(difettoDto);
            try
            {
                await _difettoService.AddDifetto(difetto);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.LogError($"{ex.Message} - INNER EXCEPTION: {ex.InnerException.Message}");
                else
                    _logger.LogError(ex.Message);

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
