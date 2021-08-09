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

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("utenti")]
    public class UtenteController : ControllerBase
    {
        private readonly ILogger<UtenteController> _logger;
        private readonly FonditalDbContext _db;

        public UtenteController(ILogger<UtenteController> logger, FonditalDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Utente> GetAllUtenti()
        {
            return _db.Utenti;
        }

        //[HttpGet]
        //public Utente GetUtenteById(int id)
        //{
        //    return _db.Utenti.SingleOrDefault(u => u.Id == id);
        //}
    }
}
