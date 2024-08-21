using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<UserDto> GetUserCredentialsAsync(string email);

        Task UpdateUserCredentialsAsync(UserDto userDto);

        Task<User?> GetUserByEmailAsync(string email);

        Task SaveUserAsync(User user);

        Task UpdateUserImageUrlAsync(string email, string imageUrl);

        Task<List<AdminUserDto>> FindAllByEmailKeywordAsync(string keyword);

        Task<List<AdminUserDto>> FindAllAsync();

        Task<AdminUserDto> FindByIdAsync(int id);

        Task UpdateUserAsync(AdminUserDto dto);

        Task CancelAllActiveUserReservationsAsync(string email);
    }
}
