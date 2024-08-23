using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.statistics.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
            CountsPerMonthDto dto = new CountsPerMonthDto();
            List<MonthCount> monthCounts = await unitOfWork.Reservations.GetReservationCountPerMonthAsync();
            FillCountsPerMonthDto(dto, monthCounts);
            return dto;
        }

        public async Task<CountsPerMonthDto> GetUsersRegistrationsCountsPerMonthAsync()
        {
            CountsPerMonthDto dto = new CountsPerMonthDto();
            List<MonthCount> monthCounts = await unitOfWork.Users.GetNumberOfRegistrationsPerMonthAsync();
            FillCountsPerMonthDto(dto, monthCounts);
            return dto;
        }

        public void FillCountsPerMonthDto(CountsPerMonthDto dto, List<MonthCount> monthCounts)
        {
            foreach (var count in monthCounts)
            {
                switch (count.Month)
                {
                    case 1:
                        dto.Jan = count.Count;
                        break;
                    case 2:
                        dto.Feb = count.Count;
                        break;
                    case 3:
                        dto.Mar = count.Count;
                        break;
                    case 4:
                        dto.Apr = count.Count;
                        break;
                    case 5:
                        dto.May = count.Count;
                        break;
                    case 6:
                        dto.Jun = count.Count;
                        break;
                    case 7:
                        dto.Jul = count.Count;
                        break;
                    case 8:
                        dto.Aug = count.Count;
                        break;
                    case 9:
                        dto.Sep = count.Count;
                        break;
                    case 10:
                        dto.Oct = count.Count;
                        break;
                    case 11:
                        dto.Nov = count.Count;
                        break;
                    case 12:
                        dto.Dec = count.Count;
                        break;
                }
            }
        }

        public async Task<TopThreeResourcesDto> GetTopThreeResourcesAsync()
        {
            List<object[]> resources = await unitOfWork.Reservations.GetReservationsWithCountsAsync();
            string firstName = "", secondName = "", thirdName = "";
            long firstCount = 0L, secondCount = 0L, thirdCount = 0L;
            if (resources.Count > 0)
            {
                firstName = (string)resources.ElementAt(0)[0];
                firstCount = (long)resources.ElementAt(0)[1];
            }
            if (resources.Count > 1)
            {
                secondName = (string)resources.ElementAt(1)[0];
                secondCount = (long)resources.ElementAt(1)[1];
            }
            if (resources.Count > 2)
            {
                thirdName = (string)resources.ElementAt(2)[0];
                thirdCount = (long)resources.ElementAt(2)[1];
            }
            return new TopThreeResourcesDto() {
                FirstCount = firstCount,
                SecondCount = secondCount,
                ThirdCount = thirdCount,
                FirstName = firstName,
                SecondName = secondName,
                ThirdName = thirdName
            };
                
        }


    }
}
