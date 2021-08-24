using Fondital.Data;
using Fondital.Services;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
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
        private readonly ServicePartnerService _spService;

        public ServicePartnerController(ILogger<ServicePartnerController> logger, FonditalDbContext db, ServicePartnerService spService)
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
    }
}
