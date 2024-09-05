using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.service
{
    public class ReservationService : GenericService<Reservation>, IReservationService
    {
        public ReservationService(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        public async Task<List<AdminReservationDto>> GetAllReservationsAsync()
        {
            List<Reservation> reservations = await unitOfWork
                .Reservations
                .FindAllWithDataAsync("ReservationId", true);
            return reservations.Select(r => new AdminReservationDto
            {
                ReservationId = r.ReservationId,
                UserEmail = r.User.Email,
                InstanceId = r.Instance.InstanceId,
                Title = r.Instance.Resource.Title,
                Start = r.ReservationStart,
                End = r.ReservationEnd,
                NumberOfExtensions = r.ExtensionCount,
                Status = r.Status,
            })
            .ToList();
        }

        public async Task<List<Reservation>> GetAllActiveReservationsAsync()
        {
            return await unitOfWork
                .Reservations
                .FindAllByReservationStatusWithInstancesAsync([ReservationStatus.Active]);
        }

        public async Task CreateReservationAsync(string userEmail, int instanceId)
        {
            ResourceInstance? instance = await unitOfWork
                .ResourceInstances
                .FindByIdWithData(instanceId)
                ?? throw new NoSuchRecordException("Instance with given id does not exist.");
            if (instance.Reserved == true)
            {
                throw new InstanceReservedException();
            }
            User? user = await unitOfWork
                .Users
                .FindByEmailAsync(userEmail)
                ?? throw new NoSuchRecordException("User with given email does not exist.");
            int reservationCount = await unitOfWork
                .Reservations
                .CountUserResourceReservationsWithStatusAsync
                (
                    instance.ResourceId,
                    userEmail,
                    [ReservationStatus.Active, ReservationStatus.Borrowed]
                );
            if (reservationCount > 0)
            {
                throw new UserAlreadyReservedResourceException();
            }
            Reservation reservation = new Reservation()
            {
                ReservationStart = DateOnly.FromDateTime(DateTime.Now),
                ReservationEnd = null,
                Status = ReservationStatus.Active,
                ExtensionCount = 0,
                UserId = user.Id,
                User = user,
                InstanceId = instanceId,
                Instance = instance,
            };
            await unitOfWork.Reservations.SaveAsync(reservation);
            user.Reservations.Add(reservation);
            instance.Reserved = true;
            instance.Reservations.Add(reservation);
            unitOfWork.ResourceInstances.Update(instance);
            unitOfWork.Users.Update(user);
            if (await unitOfWork.CompleteAsync() < 3)
            {
                throw new OperationFailedException("Failed to persist new reservation.");
            }
        }

        public async Task ExtendReservationAsync(int reservationId)
        {
            Reservation? reservation = await unitOfWork
                .Reservations
                .FindByIdAsync(reservationId)
                ?? throw new NoSuchRecordException("Reservation with given Id does not exist.");
            int maxExtension = ExtensionIntervals.MAX_NUMBER_OF_EXTENSIONS;
            int extensionLength = ExtensionIntervals.DEFAULT_RESERVATION_EXTENSION;
            if (
                (reservation.Status != ReservationStatus.Active && reservation.Status != ReservationStatus.Borrowed) ||
                reservation.ReservationEnd == null
                )
            {
                throw new OperationNotAvailableException("Reservation is not active nor borrowed.");
            }
            if (reservation.Status == ReservationStatus.Borrowed)
            {
                maxExtension = ExtensionIntervals.MAX_NUMBER_OF_BORROW_EXTENSIONS;
                extensionLength = ExtensionIntervals.DEFAULT_BORROW_EXTENSION;
            }
            if (reservation.ExtensionCount > maxExtension)
            {
                throw new OperationNotAvailableException("Extension limit reached.");
            }
            reservation.ExtensionCount += 1;
            reservation.ReservationEnd = reservation.ReservationEnd.Value.AddDays(extensionLength);
            unitOfWork.Reservations.Update(reservation);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist update.");
            }
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            Reservation? reservation = await unitOfWork
                .Reservations
                .FindByReservationIdWithInstanceAsync(reservationId)
                ?? throw new NoSuchRecordException("Reservation with given Id does not exist.");
            if (reservation.Status != ReservationStatus.Active)
            {
                throw new OperationNotAvailableException("Reservation is not active.");
            }
            reservation.Status = ReservationStatus.Cancelled;
            reservation.Instance.Reserved = false;
            unitOfWork.Reservations.Update(reservation);
            unitOfWork.ResourceInstances.Update(reservation.Instance);
            if (await unitOfWork.CompleteAsync() < 2)
            {
                throw new OperationFailedException("Failed to persist all changes.");
            }
        }

        public async Task SaveAllAsync(List<Reservation> reservations)
        {
            await unitOfWork.Reservations.SaveAllAsync(reservations);
            if (await unitOfWork.CompleteAsync() < reservations.Count)
            {
                throw new OperationFailedException("Failed to persist reservations");
            }
        }

        public async Task<List<UserReservationDto>> GetUserReservationsAsync(string userEmail)
        {
            List<Reservation> reservations = await unitOfWork.Reservations.FindAllByUserEmailWithInstancesAsync(userEmail);
            
            return reservations.Select(r => new UserReservationDto
            {
                ReservationId = r.ReservationId,
                InstanceId = r.InstanceId,
                Title = r.Instance.Resource.Title,
                Start = r.ReservationStart,
                End = r.ReservationEnd,
                NumberOfExtensions = r.ExtensionCount,
                Status = r.Status
            })
            .ToList();
        }

        public async Task<List<Reservation>> GetActiveReservationsByUserEmailAsync(string userEmail)
        {
            return await unitOfWork
                .Reservations
                .FindAllByUserEmailAndReservationStatusWithInstancesAsync(userEmail, ReservationStatus.Active);
        }

        public async Task ChangeToBorrowAsync(int reservationId)
        {
            Reservation? reservation = await unitOfWork
                .Reservations
                .FindByReservationIdWithInstanceAsync(reservationId)
                ?? throw new NoSuchRecordException("Reservation with given Id does not exist.");

            if (reservation.Status != ReservationStatus.Active)
            {
                throw new OperationNotAvailableException("Unable to lend resource - Reservation is not active.");
            }

            reservation.Status = ReservationStatus.Borrowed;
            unitOfWork.Reservations.Update(reservation);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist changes to database.");
            }
        }

        public async Task UpdateReservationAsync(AdminReservationDto dto)
        {
            int reservationId = dto.ReservationId 
                ?? throw new InvalidRequestDataException("Reservation id not provided.");
            Reservation? reservation = await unitOfWork
                .Reservations
                .FindByIdAsync(reservationId)
                ?? throw new NoSuchRecordException("Reservation with given Id does not exist.");
            DateOnly start = dto.Start ?? throw new InvalidRequestDataException("Start date not provided.");
            if (dto.Start > dto.End)
            {
                throw new InvalidRequestDataException("Invalid data passed - Start date is after end date.");
            }
            if (dto.Status == null)
            {
                throw new InvalidRequestDataException("Status not provided.");
            }
            reservation.ReservationStart = start;
            reservation.ReservationEnd = dto.End;
            if (dto.NumberOfExtensions == null || dto.NumberOfExtensions < 0)
            {
                throw new InvalidRequestDataException("Invalid number of extensions provided.");
            }
            reservation.ExtensionCount = dto.NumberOfExtensions.Value;
            reservation.Status = dto.Status.Value;
            unitOfWork.Reservations.Update(reservation);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist update.");
            }
        }
    }
}
