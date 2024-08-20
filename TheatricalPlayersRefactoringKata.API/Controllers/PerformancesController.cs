﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;

        public PerformancesController(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PerformanceRequest performanceRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _performanceService.CreatePerformance(performanceRequest);

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _performanceService.GetPerformances();

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }
    }
}
