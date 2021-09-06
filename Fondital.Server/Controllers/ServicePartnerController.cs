using Fondital.Data;
using Fondital.Services;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("servicePartners")]
    public class ServicePartnerController : ControllerBase
    {
        private readonly ILogger<ServicePartnerController> _logger;
        private readonly FonditalDbContext _db;
        private readonly IServicePartnerService _spService;

        public ServicePartnerController(ILogger<ServicePartnerController> logger, FonditalDbContext db, IServicePartnerService spService)
        {
            _logger = logger;
            _db = db;
            _spService = spService;
        }

        [HttpGet]
        public async Task<IEnumerable<ServicePartner>> Get()
        {
            return await _spService.GetAllServicePartners();
        }

		[HttpGet("utenti/{id}")]
		public async Task<ServicePartner> getWithUtentiFor(int id)
		{
			return await _spService.GetServicePartnerWithUtentiAsync(id);
		}

		[HttpPost]
        public async Task<ServicePartner> CreateServicePartner([FromBody] ServicePartner servicePartner)
        {
            ServicePartner _servicePartner = new ServicePartner();
			try
			{
                if (servicePartner == null)
                {
                    _logger.LogError("L'oggetto servicePartner inviato dal Client è null.");
                }
				else
				{
                    _servicePartner = await _spService.CreateServicePartner(servicePartner);
				}
            }
			catch (Exception e) 
            {
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
            return _servicePartner;
        }


        [HttpPut("{id}")]
        public async Task UpdateServicePartner(int id,[FromBody]ServicePartner spToUpdate)
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

         
        [HttpGet("{id}")]
        public async Task<ServicePartner> GetServicePArtnerById(int id)
		{
            ServicePartner servicePartner = new ServicePartner();
			try
			{
                servicePartner = await _spService.GetServicePartnerById(id);
			}
            catch(Exception e)
			{
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
            return servicePartner;
		}
	}
}
