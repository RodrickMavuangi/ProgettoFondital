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
    [Route("listiniControl")]
    public class ListinoController : ControllerBase
    {
        private readonly ILogger<ListinoController> _logger;
        private readonly IListinoService _listinoService;
        private readonly IConfiguration _configuration;

        public ListinoController(ILogger<ListinoController> logger, FonditalDbContext db, IListinoService listinoService, IConfiguration configuration)
        {
            _logger = logger;
            _listinoService = listinoService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Listino>> Get()
        {
            return await _listinoService.GetAllListini();
        }

        [HttpGet("{id}")]
        public async Task<Listino> GetById(int id)
        {
            return await _listinoService.GetListinoById(id);
        }

        [HttpPost("update/{listinoId}")]
        public async Task UpdateListino([FromBody] Listino listino, int listinoId)
        {
            await _listinoService.UpdateListino(listinoId, listino);
        }

        [HttpPost]
        public async Task CreateListino([FromBody] Listino listino)
        {
            await _listinoService.AddListino(listino);
        }
    }
}
