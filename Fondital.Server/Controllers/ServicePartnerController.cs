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
    [Route("servicePartnersControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class ServicePartnerController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IServicePartnerService _spService;
        private readonly IMapper _mapper;

        public ServicePartnerController(Serilog.ILogger logger, IServicePartnerService spService, IMapper mapper)
        {
            _logger = logger;
            _spService = spService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Direzione")]
        public async Task<IEnumerable<ServicePartnerDto>> Get()
        {
            return _mapper.Map<IEnumerable<ServicePartnerDto>>(await _spService.GetAllServicePartners());
        }

        [HttpGet("utenti/{id}")]
        [Authorize(Roles = "Direzione")]
        public async Task<ServicePartnerDto> GetWithUtenti(int id)
        {
            try
            {
                return _mapper.Map<ServicePartnerDto>(await _spService.GetServicePartnerWithUtentiAsync(id));
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "ServicePartner", id, ex.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ServicePartnerDto> GetServicePArtnerById(int id)
        {
            try
            {
                return _mapper.Map<ServicePartnerDto>(await _spService.GetServicePartnerById(id));
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "ServicePartner", id, ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> CreateServicePartner([FromBody] ServicePartnerDto servicePartnerDto)
        {
            ServicePartner servicePartner = _mapper.Map<ServicePartner>(servicePartnerDto);

            try
            {
                int SPId = await _spService.CreateServicePartner(servicePartner);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "ServicePartner", SPId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage} - INNER EXCEPTION: {InnerException}", "CREATE", "ServicePartner", ex.Message, ex.InnerException.Message);
                else
                    _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "ServicePartner", ex.Message);

                List<ServicePartner> list = (List<ServicePartner>)await _spService.GetAllServicePartners();

                if (list.Any(x => x.CodiceFornitore == servicePartner.CodiceFornitore))
                    return BadRequest("ErroreCodiceFornitore");
                else if (list.Any(x => x.RagioneSociale == servicePartner.RagioneSociale))
                    return BadRequest("ErroreRagioneSociale");
                else if (list.Any(x => x.CodiceCliente == servicePartner.CodiceCliente))
                    return BadRequest("ErroreCodiceCliente");
                else
                    return BadRequest("ErroreGenerico");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> UpdateServicePartner(int id, [FromBody] ServicePartnerDto spDtoToUpdate)
        {
            ServicePartner spToUpdate = _mapper.Map<ServicePartner>(spDtoToUpdate);

            try
            {
                await _spService.UpdateServicePartner(id, spToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "ServicePartner", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "ServicePartner", id, ex.Message);
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}