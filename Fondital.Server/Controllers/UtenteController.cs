using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Fondital.Data;
using Fondital.Services;
using Fondital.Shared.Services;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("utenti")]
    //[Authorize]
    public class UtenteController : ControllerBase
    {
        private readonly ILogger<UtenteController> _logger;
        private readonly FonditalDbContext _db;
        private readonly IUtenteService _ut;

        public UtenteController(ILogger<UtenteController> logger, FonditalDbContext db, IUtenteService ut)
        {
            _ut = ut;
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Utente> GetUtenti()
        {
            return _ut.GetAllUtenti().Result;

        }

        [HttpGet("{username}")]
        [AllowAnonymous]
        public Utente GetUtenteByUsername(string username)
        {
            return _db.Utenti.SingleOrDefault(u => u.UserName == username);
        }
    }
}
