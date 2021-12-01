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
        public async Task<IActionResult> GetServiceCaldaia([FromBody] CaldaiaDto caldaia)
        {
            try
            {
                //metodo dummy, ma la parte commentata funziona

                //_httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                //var response = await _httpClient.GetAsync(_config["RestClientSettings:UriModelloCaldaia"] + caldaia.Matricola);
                //if (!response.IsSuccessStatusCode)
                //    return NotFound();
                //
                //_logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Caldaia", caldaia.Matricola);
                //var listaCaldaie = await response.Content.ReadFromJsonAsync<List<CaldaiaResponseDto>>();
                //var result = _mapper.Map(listaCaldaie.First(), caldaia);
                caldaia.Model = "DRAGO";
                var result = caldaia;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Caldaia", caldaia.Matricola);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("dettagliSP")]
        public async Task<IActionResult> GetServiceSP([FromBody] ServicePartnerRequestDto sp)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = await _httpClient.GetAsync($"zapi_rapportini/getSupplierById?ID={sp.CodiceFornitore}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();
                
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Service Partner", sp.CodiceFornitore);
            
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Service Partner", sp.CodiceFornitore);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pezzoRicambio")]
        public async Task<IActionResult> GetServiceRicambio([FromBody] RicambioRequestDto request)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_config["RestClientSettings:BaseAddress"]);
                var response = await _httpClient.GetAsync($"zapi_rapportini/getSparePartById?FOR={request.Id}&MAT={request.SupplierId}&QTA={request.Quantity}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "GET", "Ricambio", request.Id);
                //var response = new RicambioDto();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Eccezione {Action} {Object} {ObjectId}", "GET", "Ricambio", request.Id);
                return BadRequest(ex.Message);
            }
        }
    }
}