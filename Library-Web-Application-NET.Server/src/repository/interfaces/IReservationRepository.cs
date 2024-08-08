using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<int> CountResourceReservationsWithStatusAsync
        (
            int resourceId,
            string userEmail,
            List<ReservationStatus> statuses
        );

        Task<List<Reservation>> FindAllByReservationStatusWithInstancesAsync
        (
            List<ReservationStatus> statuses
        );

        Task<List<Reservation>> FindAllWithDataAsync(string sortOrder);

        Task<long> CountReservationWithStatusAsync(ReservationStatus status);

        Task<long> CountReservationsByStartMonthAsync(int month);

        Task<List<object[]>> GetReservationsWithCountsAsync();
    }
}
