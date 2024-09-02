using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.dto
{
    public class UserWithRole
    {
        public User User { get; set; }

        public string RoleName { get; set; }
    }
}
