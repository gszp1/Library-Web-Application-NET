using Library_Web_Application_NET.Server.src.auth;
using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.statistics;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);

        Task<List<User>> FindByEmailKeywordAsync(string keyword);

        Task<User?> FindByUserIdAsync(int userId);

        Task<long> CountUsersAsync();

        Task<long> CountByRegistrationDateMonthAsync(int month);

        Task<List<MonthCount>> GetNumberOfRegistrationsPerMonthAsync();

        Task<List<UserWithRole>> FindUsersAndRolesByEmailKeywordAsync(string keyword);

        Task<List<UserWithRole>> FindAllUsersWithRolesAsync();
    }
}
