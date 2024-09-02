
using Library_Web_Application_NET.Server.src.auth.data;
using Library_Web_Application_NET.Server.src.auth.Interface;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library_Web_Application_NET.Server.src.auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public AuthService
        (
            ITokenService tokenService,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email.Equals(request.Email))
                    ?? throw new NoSuchRecordException("Given user does not exist.");
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                {
                    throw new InvalidCredentialsException();
                }
                return new AuthenticationResponse()
                {
                    Content = await tokenService.CreateToken(user)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest request)
        {
            try
            {
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    Name = request.Name.IsNullOrEmpty() ? null : request.Name,
                    Surname = request.Surname.IsNullOrEmpty() ? null : request.Surname,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now)
                };
                var createdUser = await userManager.CreateAsync(user, request.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return new AuthenticationResponse()
                        {
                            Content = await tokenService.CreateToken(user)
                        };
                    }
                    else
                    {
                        throw new OperationFailedException("Failed to generate JWT token.");
                    }
                }
                else
                {
                    throw new OperationFailedException("Failed to create new user.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
