using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("rapportiControl")]
    //[Authorize]
    public class RapportoController : ControllerBase
    {
        private readonly ILogger<RapportoController> _logger;
        private readonly IRapportoService _rapportoService;
        private readonly IMapper _mapper;

        public RapportoController(ILogger<RapportoController> logger, IRapportoService rapportoService, IMapper mapper)
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
            return _mapper.Map<RapportoDto>(await _rapportoService.GetRapportoById(id));
        }

        [HttpPost("update/{rapportoId}")]
        public async Task UpdateRapporto([FromBody] RapportoDto rapportoDto, int rapportoId)
        {
            Rapporto rapportoToUpdate = _mapper.Map<Rapporto>(rapportoDto);
            await _rapportoService.UpdateRapporto(rapportoId, rapportoToUpdate);
        }

        [HttpPost]
        public async Task CreateRapporto([FromBody] RapportoDto rapportoDto)
        {
            try
            {
                Rapporto newRapporto = _mapper.Map<Rapporto>(rapportoDto);
                await _rapportoService.AddRapporto(newRapporto);
            }
            catch (Exception e)
            {
                _logger.LogError("Eccezione {Action} {Object}: {ExceptionMessage}", "creazione", "rapporto", e.Message);
                throw;
            }
        }
    }
}
