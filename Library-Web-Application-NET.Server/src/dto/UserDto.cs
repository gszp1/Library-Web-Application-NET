namespace Library_Web_Application_NET.Server.src.dto
{
    public class UserDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly JoinDate { get; set; }

        public string Email { get; set; }

        public string? ImageUrl { get; set; }
    }
}
