using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.statistics;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<int> CountUserResourceReservationsWithStatusAsync(
            int resourceId,
            string userEmail,
            List<ReservationStatus> statuses
        );

        Task<List<Reservation>> FindAllByReservationStatusWithInstancesAsync(
            List<ReservationStatus> statuses
        );

        Task<Reservation?> FindByReservationIdWithInstanceAsync(int reservationId);

        Task<List<Reservation>> FindAllByUserEmailWithInstancesAsync(string userEmail);

        Task<List<Reservation>> FindAllByUserEmailAndReservationStatusWithInstancesAsync(
            string email,
            ReservationStatus status
        );

        Task<List<Reservation>> FindAllWithDataAsync(string sortBy, bool descending);

        Task<long> CountReservationsWithStatusAsync(ReservationStatus status);

        Task<long> CountReservationsByStartMonthAsync(int month);

        Task<List<object[]>> GetReservationsWithCountsAsync();

        Task<List<Reservation>> FindAllWithStatusesAsync(List<ReservationStatus> statuses);
    }
}
