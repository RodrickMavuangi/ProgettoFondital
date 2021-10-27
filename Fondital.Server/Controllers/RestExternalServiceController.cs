using AutoMapper;
using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
    [ApiController]
    [Route("externalServiceController")]
    [Authorize(Roles = "Direzione,Service Partner")]
    public class RestExternalServiceController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RestExternalServiceController(Serilog.ILogger logger, IConfiguration config, HttpClient httpClient, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        [HttpPost("modelloCaldaia")]
        public async Task<IActionResult> GetServiceCaldaia([FromBody] CaldaiaDto caldaiaDto)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = await _httpClient.GetAsync(_config["RestClientSettings:UriModelloCaldaia"] + caldaiaDto.Matricola);
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Caldaia", caldaiaDto.Matricola);
                var listaCaldaie = await response.Content.ReadFromJsonAsync<List<CaldaiaResponseDto>>();
                var result = _mapper.Map(listaCaldaie.First(), caldaiaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Caldaia", caldaiaDto.Matricola);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pezzoRicambio/{idRicambio}/{idFornitore}/{quantita}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceRicambio(RicambioRequestDto ricambio)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = await _httpClient.GetAsync($"/sparePart/{ricambio.Id}/{ricambio.SupplierId}/{ricambio.Quantity}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Ricambio", ricambio.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Ricambio", ricambio.Id);
                return BadRequest(ex.Message);
            }
        }
    }
}