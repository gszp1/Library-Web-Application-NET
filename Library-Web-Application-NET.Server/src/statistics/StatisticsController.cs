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
        public ActionResult<UserStatisticsDto> GetUserStatistics() { }

        [HttpGet("resources")]
        public ActionResult<ResourceStatisticsDto> GetResourceStatistics() { }

        [HttpGet("reservations/monthCounts")]
        public ActionResult<CountsPerMonthDto> GetReservationCounts() { }

        [HttpGet("registrations/monthCounts")]
        public ActionResult<CountsPerMonthDto> GetRegistrationsCounts() { }

        [HttpGet("reservations/top3")]
        public ActionResult<TopThreeResourcesDto> GetTopThreeReservationCounts() { }
    }
}
