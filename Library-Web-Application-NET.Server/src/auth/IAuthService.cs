namespace Library_Web_Application_NET.Server.src.auth
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> Register(RegisterRequest request);

        Task<AuthenticationResponse> Login(LoginRequest request);
    }
}
