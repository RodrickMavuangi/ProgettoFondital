using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
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

        public RestExternalServiceController(Serilog.ILogger logger, IConfiguration config, HttpClient httpClient)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
        }

        [HttpGet("modelloCaldaia/{matricola}")]
        public async Task<IActionResult> GetServiceCaldaia(string matricola)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = await _httpClient.GetAsync($"/getProductById?ID={matricola}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Caldaia", "caldaiaId");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Caldaia", "caldaiaId");
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

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Ricambio", "ricambioId");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Ricambio", "ricambioId");
                return BadRequest(ex.Message);
            }
        }
    }
}