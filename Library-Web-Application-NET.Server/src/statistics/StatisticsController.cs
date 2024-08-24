using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.statistics
{

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService statisticsService;

        public StatisticsController(StatisticsService statisticsService) 
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<UserStatisticsDto>> GetUserStatistics() 
        {
            var res = await statisticsService.GetUserStatisticsAsync();
            return Ok(res);
        }

        [HttpGet("resources")]
        public async Task<ActionResult<ResourceStatisticsDto>> GetResourceStatistics()
        {
            return Ok(await statisticsService.GetResourceStatisticsAsync());
        }

        [HttpGet("reservations/monthCounts")]
        public async Task<ActionResult<CountsPerMonthDto>> GetReservationCounts()
        {
            return Ok(await statisticsService.GetReservationCountsPerMonthAsync());
        }

        [HttpGet("registrations/monthCounts")]
        public async Task<ActionResult<CountsPerMonthDto>> GetRegistrationsCounts() {
            return Ok(await statisticsService.GetUsersRegistrationsCountsPerMonthAsync());
        }

        [HttpGet("reservations/top3")]
        public async Task<ActionResult<TopThreeResourcesDto>> GetTopThreeReservationCounts()
        {
            return Ok(await statisticsService.GetTopThreeResourcesAsync());
        }
    }
}
