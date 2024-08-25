using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.dto
{
    public class AdminInstanceDto
    {
        public int ResourceId { get; set; }

        public int Id { get; set; }

        public bool IsReserved { get; set; }

        public InstanceStatus InstanceStatus { get; set; }
    }
}
