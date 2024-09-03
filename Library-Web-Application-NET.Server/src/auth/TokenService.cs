using Library_Web_Application_NET.Server.src.auth.data;
using Library_Web_Application_NET.Server.src.auth.Interface;
using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_Web_Application_NET.Server.src.auth
{

    public class TokenService : ITokenService
    {

        private readonly IConfiguration configuration;

        private readonly SymmetricSecurityKey key;

        private readonly UserManager<User> userManager;

        private readonly RoleManager<UserRole> roleManager;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            this.configuration = configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var claims = await roleManager.GetClaimsAsync(await roleManager.FindByNameAsync(role));
                roleClaims.AddRange(claims);
                roleClaims.Add(new Claim("Role", role));
                roleClaims.Add(new Claim("sub", user.Email));
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            DateTime expirationTime;
            try
            {
                expirationTime = DateTime.Now.AddMinutes(int.Parse(configuration["JWT:ExpiryMinutes"]));
            }
            catch (Exception)
            {
                expirationTime = DateTime.Now.AddMinutes(60);
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(roleClaims),
                Expires = expirationTime,
                SigningCredentials = creds,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
