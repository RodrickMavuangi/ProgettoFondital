using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("servicePartnersControl")]
    public class ServicePartnerController : ControllerBase
    {
        private readonly ILogger<ServicePartnerController> _logger;
        private readonly IServicePartnerService _spService;
        private readonly IMapper _mapper;

        public ServicePartnerController(ILogger<ServicePartnerController> logger, IServicePartnerService spService, IMapper mapper)
        {
            _logger = logger;
            _spService = spService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ServicePartnerDto>> Get()
        {
            return _mapper.Map<IEnumerable<ServicePartnerDto>>(await _spService.GetAllServicePartners());
        }

		[HttpGet("utenti/{id}")]
		public async Task<ServicePartnerDto> getWithUtentiFor(int id)
		{
			return _mapper.Map<ServicePartnerDto>(await _spService.GetServicePartnerWithUtentiAsync(id));
		}

        /*
		[HttpPost]
        public async Task<ServicePartnerDto> CreateServicePartner([FromBody] ServicePartnerDto servicePartnerDto)
        {
            ServicePartner servicePartner = _mapper.Map<ServicePartner>(servicePartnerDto);
            ServicePartnerDto newServicePartner = new ServicePartnerDto();
			try
			{
                if (servicePartner == null)
                {
                    _logger.LogError("L'oggetto servicePartner inviato dal Client è null.");
                }
				else
				{
                    newServicePartner = _mapper.Map<ServicePartnerDto>(await _spService.CreateServicePartner(servicePartner));
				}
            }
			catch (Exception e) 
            {
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
            return newServicePartner;
        }
        */

        [HttpPost]
        public async Task<IActionResult> CreateServicePartner([FromBody] ServicePartnerDto servicePartnerDto)
        {
            ServicePartner servicePartner = _mapper.Map<ServicePartner>(servicePartnerDto);
            try
            {
                await _spService.CreateServicePartner(servicePartner);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _logger.LogError($"{ex.Message} - INNER EXCEPTION: {ex.InnerException.Message}");
                else
                    _logger.LogError(ex.Message);

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


        /*
        [HttpPut("{id}")]
        public async Task UpdateServicePartner(int id,[FromBody] ServicePartnerDto spToUpdate)
		{
            ServicePartner _servicePartner = new ServicePartner();
            try
            {
                if(spToUpdate == null)
				{
                    _logger.LogError("L'oggetto servicePartner inviato dal Client è null.");
				}
				else
				{
                    _servicePartner = await _spService.GetServicePartnerById(id);

                    if(_servicePartner == null)
					{
                        _logger.LogError($"il service partner con l'id:{spToUpdate.Id} non è stato trovato");
					}
					else
					{
                         await _spService.UpdateServicePartner(_servicePartner,spToUpdate);
					}
                }
			}
            catch(Exception e)
			{
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
		}
        */

        [HttpPut("{id}")]
        public async Task UpdateServicePartner(int id, [FromBody]ServicePartnerDto spDtoToUpdate)
        {
            ServicePartner spToUpdate = _mapper.Map<ServicePartner>(spDtoToUpdate);
            await _spService.UpdateServicePartner(id, spToUpdate);
        }

         
        [HttpGet("{id}")]
        public async Task<ServicePartnerDto> GetServicePArtnerById(int id)
		{
            ServicePartnerDto servicePartnerDto = new ServicePartnerDto();
			try
			{
                servicePartnerDto = _mapper.Map<ServicePartnerDto>(await _spService.GetServicePartnerById(id));
			}
            catch(Exception e)
			{
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
            return servicePartnerDto;
		}
	}
}
