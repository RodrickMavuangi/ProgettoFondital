using AutoMapper;
using Fondital.Data;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public VoceCostoController(ILogger<VoceCostoController> logger, FonditalDbContext db, IVoceCostoService voceCostoService, IConfiguration configuration, IMapper mapper)
        {
            _logger = logger;
            _voceCostoService = voceCostoService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VoceCostoDto>> Get([FromQuery] bool? isEnabled)
        {
            return _mapper.Map<IEnumerable<VoceCostoDto>>(await _voceCostoService.GetAllVociCosto(isEnabled));
        }

        [HttpPost("update/{voceCostoId}")]
        public async Task UpdateVoceCosto([FromBody] VoceCostoDto voceCostoDto, int voceCostoId)
        {
            VoceCosto voceCostoToUpdate = _mapper.Map<VoceCosto>(voceCostoDto);
            await _voceCostoService.UpdateVoceCosto(voceCostoId, voceCostoToUpdate);
        }

        [HttpPost]
        public async Task CreateVoceCosto([FromBody] VoceCostoDto voceCostoDto)
        {
            VoceCosto voceCosto = _mapper.Map<VoceCosto>(voceCostoDto);
            await _voceCostoService.AddVoceCosto(voceCosto);
        }
    }
}
