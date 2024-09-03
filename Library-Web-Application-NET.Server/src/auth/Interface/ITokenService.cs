using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.auth.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
