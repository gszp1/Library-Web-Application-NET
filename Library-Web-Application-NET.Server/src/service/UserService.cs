using Library_Web_Application_NET.Server.src.auth.data;
using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Library_Web_Application_NET.Server.src.service
{
    public class UserService : GenericService<User>, IUserService
    {

        private readonly UserManager<User> userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
        {
            this.userManager = userManager;
        }

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
            var usersWithRoles = await unitOfWork.Users.FindUsersAndRolesByEmailKeywordAsync(keyword);

            var userDtos = usersWithRoles.Select(u => new AdminUserDto
            {
                Id = u.User.Id,
                Name = u.User.Name,
                Surname = u.User.Surname,
                PhoneNumber = u.User.PhoneNumber,
                JoinDate = u.User.JoinDate,
                Email = u.User.Email,
                ImageUrl = u.User.ImageUrl,
                Status = u.User.Status,
                Role = (Role)Enum.Parse(typeof(Role), u.RoleName)
            }).ToList();

            return userDtos;
        }

        public async Task<List<AdminUserDto>> FindAllAsync()
        {
            var users = await unitOfWork.Users.FindAllUsersWithRolesAsync();
            return users.Select(u => new AdminUserDto
            {
                Id = u.User.Id,
                Name = u.User.Name,
                Surname = u.User.Surname,
                PhoneNumber = u.User.PhoneNumber,
                JoinDate = u.User.JoinDate,
                Email = u.User.Email,
                ImageUrl = u.User.ImageUrl,
                Status = u.User.Status,
                Role = (Role)Enum.Parse(typeof(Role), u.RoleName)
            })
            .ToList();
        }

        public async Task<AdminUserDto> FindByIdAsync(int id)
        {
            User? user = await unitOfWork
                .Users
                .FindByUserIdAsync(id)
                ?? throw new NoSuchRecordException("User with given id does not exist.");
            var role = await (from ur in unitOfWork.Context.UserRoles
                              join r in unitOfWork.Context.Roles on ur.RoleId equals r.Id
                              where ur.UserId == user.Id
                              select r.Name).FirstOrDefaultAsync();

            return new AdminUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                JoinDate = user.JoinDate,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                Status = user.Status,
                Role = (Role)Enum.Parse(typeof(Role), role)
            };
        }

        public async Task UpdateUserAsync(AdminUserDto dto)
        {
            User? user = await unitOfWork
                 .Users
                 .FindByIdAsync(dto.Id)
                 ?? throw new NoSuchRecordException("User with given Email does not exist.");

            string? oldRoleString = await unitOfWork.Context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Join(unitOfWork.Context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                .FirstOrDefaultAsync();
            var oldStatus = user.Status;

            Role oldRole = (Role)Enum.Parse(typeof(Role), oldRoleString);


            UpdateUserWithDto(user, dto);
            
            if (dto.Role != oldRole)
            {
                await UpdateUserRoleAsync(user.Id, dto.Role.ToString());
            }
            
            unitOfWork.Users.Update(user);
            if (user.Status != dto.Status || dto.Role != oldRole)
            {
                await CancelAllActiveUserReservationsAsync(user.Email);
            }
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex )
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
            try
            {
                await unitOfWork.CompleteAsync();
            } catch (Exception e)
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
        }

        private async Task UpdateUserRoleAsync(int userId, string newRoleName)
        {
            var role = await unitOfWork.Context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(newRoleName));
            if (role == null)
            {
                throw new NoSuchRecordException("Role does not exist.");
            }

            var userRole = await unitOfWork.Context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);
            if (userRole != null)
            {
                unitOfWork.Context.UserRoles.Remove(userRole);
                await unitOfWork.CompleteAsync();
            }
            unitOfWork.Context.UserRoles.Add(new IdentityUserRole<int> { UserId = userId, RoleId = role.Id });
        }
    }
}
