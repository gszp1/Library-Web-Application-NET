using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.auth
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
