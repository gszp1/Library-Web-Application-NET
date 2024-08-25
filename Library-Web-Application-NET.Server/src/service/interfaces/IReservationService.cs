using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IReservationService : IGenericService<Reservation>
    {
        Task<List<AdminReservationDto>> GetAllReservationsAsync();

        Task<List<Reservation>> GetAllActiveReservationsAsync();

        Task CreateReservationAsync(string UserEmail, int instanceId);

        Task ExtendReservationAsync(int reservationId);

        Task CancelReservationAsync(int reservationId);

        Task SaveAllAsync(List<Reservation> reservations);

        Task<List<UserReservationDto>> GetUserReservationsAsync(string userEmail);
    
        Task<List<Reservation>> GetActiveReservationsByUserEmailAsync(string userEmail);

        Task ChangeToBorrowAsync(int reservationId);

        Task UpdateReservationAsync(AdminReservationDto dto);
    }

}
