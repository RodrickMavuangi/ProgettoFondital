﻿using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
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
    [Route("listiniControl")]
    [Authorize]
    public class ListinoController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IListinoService _listinoService;
        private readonly IMapper _mapper;

        public ListinoController(Serilog.ILogger logger, IListinoService listinoService, IMapper mapper)
        {
            _logger = logger;
            _listinoService = listinoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ListinoDto>> Get()
        {
            return _mapper.Map<IEnumerable<ListinoDto>>(await _listinoService.GetAllListini());
        }

        [HttpGet("{id}")]
        public async Task<ListinoDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<ListinoDto>(await _listinoService.GetListinoById(id));
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "GET", "Listino", id, ex.Message);
                throw;
            }
        }

        [HttpPost("update/{listinoId}")]
        public async Task UpdateListino([FromBody] ListinoDto listinoDto, int listinoId)
        {
            Listino listinoToUpdate = _mapper.Map<Listino>(listinoDto);

            try
            {
                await _listinoService.UpdateListino(listinoId, listinoToUpdate);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "UPDATE", "Listino", listinoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "UPDATE", "Listino", listinoId, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task CreateListino([FromBody] ListinoDto listinoDto)
        {
            Listino newListino = _mapper.Map<Listino>(listinoDto);

            try
            {
                int listinoId = await _listinoService.AddListino(newListino);
                _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CREATE", "Listino", listinoId);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object}: {ExceptionMessage}", "CREATE", "Listino", ex.Message);
                throw;
            }
        }
    }
}