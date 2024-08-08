using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<int> CountResourceReservationsWithStatus
        (
            int resourceId,
            string userEmail,
            List<ReservationStatus> statuses
        );

        Task<List<Reservation>> FindAllByReservationStatusWithInstances
        (
            List<ReservationStatus> statuses
        );

        Task<List<Reservation>> FindAllWithData(string sortOrder);

        Task<long> CountReservationWithStatus(ReservationStatus status);

        Task<long> CountReservationsByStartMonth(int month);

        Task<List<object[]>> GetReservationsWithCounts();
    }
}
