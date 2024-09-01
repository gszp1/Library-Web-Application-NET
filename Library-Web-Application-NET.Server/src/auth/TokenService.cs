using Library_Web_Application_NET.Server.src.model;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library_Web_Application_NET.Server.src.auth
{

    public class TokenService : ITokenService
    {

        private readonly IConfiguration configuration;

        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        }

        public string CreateToken(User user)
        {
            var claims
        }
    }
}
