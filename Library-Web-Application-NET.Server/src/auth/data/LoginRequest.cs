using System.ComponentModel.DataAnnotations;

namespace Library_Web_Application_NET.Server.src.auth.data
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
