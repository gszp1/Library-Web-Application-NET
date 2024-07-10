using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.model
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ImageUrl { get; set; }
        
        public DateOnly JoinDate { get; set; }

        public UserStatus Status { get; set; }

        public Role Role { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
