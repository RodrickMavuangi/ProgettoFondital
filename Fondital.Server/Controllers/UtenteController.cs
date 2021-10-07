using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("utentiControl")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class UtenteController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IUtenteService _ut;
        private readonly IMapper _mapper;

        public UtenteController(Serilog.ILogger logger, IUtenteService ut, IMapper mapper)
        {
            _ut = ut;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Direzione")]
        public IEnumerable<UtenteDto> GetUtenti([FromQuery]bool? isDirezione)
        {
            return _mapper.Map<IEnumerable<UtenteDto>>(_ut.GetAllUtenti(isDirezione).Result);
        }

        [HttpGet("getsingle/{username}")]
        public UtenteDto GetUtenteByUsername(string username)
        {
            try
            {
                return _mapper.Map<UtenteDto>(_ut.GetUtenteByUsername(username).Result);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "Utente", username, ex.Message);
                throw;
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> UpdateUtente([FromBody] UtenteDto utenteDtoToUpdate)
        {
            Utente utenteToUpdate = _mapper.Map<Utente>(utenteDtoToUpdate);

            try
            {
                await _ut.UpdateUtente(utenteToUpdate.UserName, utenteToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Utente", utenteToUpdate.UserName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Utente", utenteDtoToUpdate.UserName, ex.Message);
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}