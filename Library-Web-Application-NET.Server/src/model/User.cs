using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Identity;

namespace Library_Web_Application_NET.Server.src.model
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? ImageUrl { get; set; }
        
        public DateOnly JoinDate { get; set; }

        public UserStatus Status { get; set; }

        public Role Role { get; set; }

        public List<Reservation> Reservations { get; set; } = [];
    }
}
