namespace Library_Web_Application_NET.Server.src.statistics.interfaces
{
    public interface IStatisticsService
    {

        Task<UserStatisticsDto> GetUserStatisticsAsync();

        Task<ResourceStatisticsDto> GetResourceStatisticsAsync();

        Task<CountsPerMonthDto> GetReservationCountsPerMonthAsync();

        Task<CountsPerMonthDto> GetUsersRegistrationsCountsPerMonthAsync();

        Task<TopThreeResourcesDto> GetTopThreeResourcesAsync();
    }
}
