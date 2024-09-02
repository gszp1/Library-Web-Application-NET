using Library_Web_Application_NET.Server.src.auth.data;

namespace Library_Web_Application_NET.Server.src.auth.Interface
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> Register(RegisterRequest request);

        Task<AuthenticationResponse> Login(LoginRequest request);
    }
}
