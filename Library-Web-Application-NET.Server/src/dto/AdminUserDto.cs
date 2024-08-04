using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.dto
{
    public class AdminUserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly JoinDate { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public UserStatus Status { get; set; }

        public Role Role { get; set; }  
    }
}
