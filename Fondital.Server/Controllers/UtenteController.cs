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
    [Route("utentiControl")]
    //[Authorize]
    public class UtenteController : ControllerBase
    {
        private readonly ILogger<UtenteController> _logger;
        private readonly IUtenteService _ut;

        public UtenteController(ILogger<UtenteController> logger, IUtenteService ut)
        {
            _ut = ut;
            _logger = logger;
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
            return _ut.GetUtenteByUsername(username).Result;
        }

        [HttpPost("PwUpdated/{username}")]
        public async Task UpdateDataCambioPw(string username)
        {
            try
            {
                var utente = this.GetUtenteByUsername(username);
                utente.Pw_LastChanged = DateTime.Now;
                utente.Pw_MustChange = false;
                await _ut.UpdateUtente(utente.UserName, utente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore nell'aggiornamento dell'ultimo cambio password per l'utente {username}.");
                throw;
            }
        }


        [HttpPut("{utenteId}")]
        public async Task UpdateUtente(int utenteId, [FromBody] Utente utenteToUpdate)
        {
            Utente _utente = new Utente();
            try
            {
                if (utenteToUpdate == null)
                {
                    _logger.LogError("L'oggetto servicePartner inviato dal Client è null.");
                }
                else
                {
                    _utente = await _ut.GetUtenteById(utenteId);

                    if (_utente == null)
                    {
                        _logger.LogError($"l'utente con l'id:{utenteToUpdate.Id} non è stato trovato");
                    }
                    else
                    {
                        Utente utenteFromDB = await _ut.GetUtenteById(utenteId);
                        await _ut.UpdateUtente(utenteToUpdate,utenteFromDB);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
        }
    }
}
