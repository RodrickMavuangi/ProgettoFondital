using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("utentiControl")]
    //[Authorize]
    public class UtenteController : ControllerBase
    {
        private readonly ILogger<UtenteController> _logger;
        private readonly IUtenteService _ut;
        private readonly IMapper _mapper;

        public UtenteController(ILogger<UtenteController> logger, IUtenteService ut, IMapper mapper)
        {
            _ut = ut;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<UtenteDto> GetUtenti()
        {
            return _mapper.Map<IEnumerable<UtenteDto>>(_ut.GetAllUtenti().Result);
        }

        [HttpGet("{username}")]
        public UtenteDto GetUtenteByUsername(string username)
        {
            return _mapper.Map<UtenteDto>(_ut.GetUtenteByUsername(username).Result);
        }

        /*
        [HttpPut("{id}")]
        public async Task UpdateUtente(int id, [FromBody] UtenteDto utenteDtoToUpdate)
        {
            Utente utenteToUpdate = _mapper.Map<Utente>(utenteDtoToUpdate);
            Utente _utente = new Utente();
            try
            {
                if (utenteToUpdate == null)
                {
                    _logger.LogError("L'oggetto servicePartner inviato dal Client è null.");
                }
                else
                {
                    _utente = await _ut.GetUtenteById(id);

                    if (_utente == null)
                    {
                        _logger.LogError($"l'utente con l'id:{utenteToUpdate.Id} non è stato trovato");
                    }
                    else
                    {
                        Utente utenteFromDB = await _ut.GetUtenteById(id);
                        await _ut.UpdateUtente(utenteToUpdate,_utente);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"C'è stato un problema : {e.Message}");
            }
        }
        */

        [HttpPut("{id}")]
        public async Task UpdateUtente(int id, [FromBody] UtenteDto utenteDtoToUpdate)
        {
            Utente utenteToUpdate = _mapper.Map<Utente>(utenteDtoToUpdate);
            Utente utente = await _ut.GetUtenteById(id);
            await _ut.UpdateUtente(utente.UserName, utenteToUpdate);
        }
    }
}