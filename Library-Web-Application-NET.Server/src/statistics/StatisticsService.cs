using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.statistics.interfaces;
using Library_Web_Application_NET.Server.src.util;
using System.Reflection.Metadata.Ecma335;

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
            long userCount = await unitOfWork.Users.CountUsersAsync();
            List<Reservation> reservations = await unitOfWork.Reservations.FindAllAsync();
            long reservationCount = reservations.Count();
            long reservationsLengthSum = reservations.Select(r =>
            {
                if (r.ReservationEnd == null)
                {
                    return 0;
                }
                DateTime dateStart = r.ReservationStart.ToDateTime(TimeOnly.Parse("00:00AM"));
                DateTime dateEnd = r.ReservationEnd.Value.ToDateTime(TimeOnly.Parse("00:00AM"));
                return (dateEnd - dateStart).Days;
            }).Sum();
            return new UserStatisticsDto
            {
                NumberOfUsers = userCount,
                AvgNumberOfReservations = (long)(userCount == 0 ? 0 : Math.Round((double)reservationCount / userCount, 0)),
                AvgReservationLength = reservationsLengthSum == 0 ? 0 : (double)reservationsLengthSum / reservationCount
            };
        }

        public async Task<ResourceStatisticsDto> GetResourceStatisticsAsync()
        {
            List<Resource> resources = await unitOfWork.Resources.FindAllAsync();
            List<ResourceInstance> resourceInstances = await unitOfWork.ResourceInstances.FindAllAsync();
            List<Reservation> reservations = await unitOfWork
                .Reservations
                .FindAllWithStatusesAsync([ReservationStatus.Active, ReservationStatus.Borrowed]);
            return new ResourceStatisticsDto()
            {
                NumberOfResources = resources.Count(),
                NumberOfInstances = resourceInstances.Count(),
                ReservedInstances = reservations.Where(r => r.Status == ReservationStatus.Active).Count(),
                BorrowedInstances = reservations.Where(r => r.Status == ReservationStatus.Borrowed).Count()
            };
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
