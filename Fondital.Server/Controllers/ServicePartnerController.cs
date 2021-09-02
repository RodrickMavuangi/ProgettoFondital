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
    [Route("servicePartnersControl")]
    public class ServicePartnerController : ControllerBase
    {
        private readonly ILogger<ServicePartnerController> _logger;
        private readonly IServicePartnerService _spService;

        public ServicePartnerController(ILogger<ServicePartnerController> logger, FonditalDbContext db, IServicePartnerService spService)
        {
            _logger = logger;
            _spService = spService;
        }

        [HttpGet]
        public async Task<IEnumerable<ServicePartner>> Get()
        {
            return await _spService.GetAllServicePartners();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePartner>> GetByIdWithUtenti(int id)
        {
            return await _spService.GetServicePartnerByIdWithUtenti(id);
        }
    }
}
