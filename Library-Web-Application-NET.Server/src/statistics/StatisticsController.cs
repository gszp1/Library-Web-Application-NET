﻿using Library_Web_Application_NET.Server.src.statistics.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.statistics
{

    [Route("api/statistics")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService) 
        {
            this.statisticsService = statisticsService;
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<UserStatisticsDto>> GetUserStatistics() 
        {
            var res = await statisticsService.GetUserStatisticsAsync();
            return Ok(res);
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet]
        [Route("resources")]
        public async Task<ActionResult<ResourceStatisticsDto>> GetResourceStatistics()
        {
            return Ok(await statisticsService.GetResourceStatisticsAsync());
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet]
        [Route("reservations/monthCounts")]
        public async Task<ActionResult<CountsPerMonthDto>> GetReservationCounts()
        {
            return Ok(await statisticsService.GetReservationCountsPerMonthAsync());
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet]
        [Route("registrations/monthCounts")]
        public async Task<ActionResult<CountsPerMonthDto>> GetRegistrationsCounts() {
            return Ok(await statisticsService.GetUsersRegistrationsCountsPerMonthAsync());
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet]
        [Route("reservations/top3")]
        public async Task<ActionResult<TopThreeResourcesDto>> GetTopThreeReservationCounts()
        {
            return Ok(await statisticsService.GetTopThreeResourcesAsync());
        }
    }
}
