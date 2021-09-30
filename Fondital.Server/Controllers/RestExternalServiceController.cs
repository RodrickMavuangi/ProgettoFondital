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
    [Authorize]
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

        [HttpGet("modelloCaldaia")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceCaldaia()
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _httpClient.GetAsync(_config["RestClientSettings:UriModelloCaldaia"]);
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Caldaia", "caldaiaId");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "Caldaia", "caldaiaId", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pezzoRicambio")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceRicambio()
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = _httpClient.GetAsync(_config["RestClientSettings:UriRicambio"]).Result;
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Ricambio", "ricambioId");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "Ricambio", "ricambioId", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}