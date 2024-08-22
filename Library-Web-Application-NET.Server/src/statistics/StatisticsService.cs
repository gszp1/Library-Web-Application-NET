using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.statistics.interfaces;

namespace Library_Web_Application_NET.Server.src.statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserStatisticsDto> GetUserStatisticsAsync()
        {
        }

        public async Task<ResourceStatisticsDto> GetResourceStatisticsAsync()
        {

        }

        public async Task<CountsPerMonthDto> GetReservationCountsPerMonthAsync()
        {

        }

        public async Task<CountsPerMonthDto> GetUsersRegistrationsCountsPerMonthAsync()
        {

        }

        public async Task<TopThreeResourcesDto> GetTopThreeResourcesAsync()
        {

        }


    }
}
