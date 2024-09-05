
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
                var user = await userManager.FindByEmailAsync(request.Email)
                    ?? throw new NoSuchRecordException("Given user does not exist.");
                Console.WriteLine("Passed user exists");
                if (user.Status == util.UserStatus.Closed)
                {
                    throw new OperationNotAvailableException("Failed to authenticate");
                }
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                {
                    Console.Write(result.ToString() + "\n");
                    throw new InvalidCredentialsException();
                }
                return new AuthenticationResponse()
                {
                    Content = await tokenService.CreateToken(user)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("ex1");
                Console.WriteLine(e.StackTrace);
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
                    JoinDate = DateOnly.FromDateTime(DateTime.Now),
                    PhoneNumber = request.PhoneNumber.IsNullOrEmpty() ? null : request.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    EmailConfirmed = true
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
