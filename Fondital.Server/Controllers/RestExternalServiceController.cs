using Fondital.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly RestClientSettings _restClientSettings;

        public RestExternalServiceController(Serilog.ILogger logger, HttpClient httpClient, IOptions<RestClientSettings> restClientSettings)
        {
            _logger = logger;
            _httpClient = httpClient;
            _restClientSettings = restClientSettings.Value;
        }

        [HttpGet("modelloCaldaia")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceCaldaia()
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_restClientSettings.BaseAddress);
                //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = _httpClient.GetAsync(_restClientSettings.UriModelloCaldaia).Result;
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Caldaia", "caldaiaId");
                return Ok();
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
                _httpClient.BaseAddress = new Uri(_restClientSettings.BaseAddress);
                var response = _httpClient.GetAsync(_restClientSettings.UriRicambio).Result;
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