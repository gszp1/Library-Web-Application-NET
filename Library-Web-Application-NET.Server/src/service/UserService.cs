using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;

namespace Library_Web_Application_NET.Server.src.service
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        public async Task<UserDto> GetUserCredentialsAsync(string email)
        {
            User? user = await unitOfWork
                .Users
                .FindByEmailAsync(email)
                ?? throw new NoSuchRecordException("User with given Email does not exist.");
            return new UserDto()
            {
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                JoinDate = user.JoinDate,
                Email = user.Email,
                ImageUrl = user.ImageUrl
            };
        }

        public async Task UpdateUserCredentialsAsync(UserDto userDto)
        {
            User? user = await unitOfWork
                .Users
                .FindByEmailAsync(userDto.Email)
                ?? throw new NoSuchRecordException("User with given Email does not exist.");
            user.Name = userDto.Name;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Surname = userDto.Surname;
            unitOfWork.Users.Update(user);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to update user.");
            }
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            User? user =  await unitOfWork.Users.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            return new UserDto()
            {
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                JoinDate = user.JoinDate,
                Email = user.Email,
                ImageUrl = user.ImageUrl
            };
        }

        public async Task SaveUserAsync(User user)
        {
            await unitOfWork.Users.SaveAsync(user);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist user to database.");
            }
        }

        public async Task UpdateUserImageUrlAsync(string email, string imageUrl)
        {
            User? user = await unitOfWork
                 .Users
                 .FindByEmailAsync(email)
                 ?? throw new NoSuchRecordException("User with given Email does not exist.");
            user.ImageUrl = imageUrl;
            unitOfWork.Users.Update(user);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist url change.");
            }
        }

        public async Task<List<AdminUserDto>> FindAllByEmailKeywordAsync(string keyword)
        {
            List<User> users = await unitOfWork.Users.FindByEmailKeywordAsync(keyword);
            return users.Select(u => new AdminUserDto
            {
                Id = u.UserId,
                Name = u.Name,
                Surname = u.Surname,
                PhoneNumber = u.PhoneNumber,
                JoinDate = u.JoinDate,
                Email = u.Email,
                ImageUrl = u.ImageUrl,
                Status = u.Status,
                Role = u.Role
            })
            .ToList();
        }

        public async Task<List<AdminUserDto>> FindAllAsync()
        {
            List<User> users = await unitOfWork.Users.FindAllAsync();
            return users.Select(u => new AdminUserDto
            {
                Id = u.UserId,
                Name = u.Name,
                Surname = u.Surname,
                PhoneNumber = u.PhoneNumber,
                JoinDate = u.JoinDate,
                Email = u.Email,
                ImageUrl = u.ImageUrl,
                Status = u.Status,
                Role = u.Role
            })
            .ToList();
        }

        public async Task<AdminUserDto> FindByIdAsync(int id)
        {
            User? user = await unitOfWork
                .Users
                .FindByUserIdAsync(id)
                ?? throw new NoSuchRecordException("User with given id does not exist.");
            return new AdminUserDto
            {
                Id = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                JoinDate = user.JoinDate,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                Status = user.Status,
                Role = user.Role
            };
        }

        public async Task UpdateUserAsync(AdminUserDto dto)
        {
            User? user = await unitOfWork
                 .Users
                 .FindByEmailAsync(dto.Email)
                 ?? throw new NoSuchRecordException("User with given Email does not exist.");
            UpdateUserWithDto(user, dto);
            unitOfWork.Users.Update(user);
            if (user.Status != dto.Status || user.Role != dto.Role)
            {
                await CancelAllActiveUserReservationsAsync(user.Email);
            }
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist changes.");
            }
        }

        public async Task CancelAllActiveUserReservationsAsync(string email)
        {
            List<Reservation> reservations = await unitOfWork
                .Reservations
                .FindAllByUserEmailAndReservationStatusWithInstancesAsync(email, util.ReservationStatus.Active);
            List<ResourceInstance> instances = [];
            reservations.ForEach(reservation =>
            {
                reservation.Status = util.ReservationStatus.Cancelled;
                ResourceInstance instance = reservation.Instance;
                if (instance.Reserved == true)
                {
                    instance.Reserved = false;
                    instances.Add(instance);
                }
            });
            unitOfWork.Reservations.UpdateRange(reservations);
            unitOfWork.ResourceInstances.UpdateRange(instances);
            if (await unitOfWork.CompleteAsync() < instances.Count + reservations.Count)
            {
                throw new OperationFailedException("Failed to persist changes.");
            }   
        }

        public void UpdateUserWithDto(User user, AdminUserDto dto)
        {
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.ImageUrl = dto.ImageUrl;
            user.Status = dto.Status;
            user.Role = dto.Role;
        }
    }
}
