using Fondital.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
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
    [Route("utenti")]
    public class UtenteController : ControllerBase
    {
        private readonly ILogger<UtenteController> _logger;
        private readonly FonditalDBContext _db;

        public UtenteController(ILogger<UtenteController> logger, FonditalDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Utente> Get()
        {
            return _db.Utenti;
        }
    }
}
