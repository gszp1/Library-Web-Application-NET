using System.ComponentModel.DataAnnotations;

namespace Library_Web_Application_NET.Server.src.auth.data
{
    public class RegisterRequest
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
