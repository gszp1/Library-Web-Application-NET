using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.model
{
    public class ResourceInstance
    {
        public int InstanceId { get; set; }

        public bool Reserved { get; set; }

        public InstanceStatus Status { get; set; }
        
        public int ResourceId { get; set; }

        public Resource Resource { get; set; }

        public List<Reservation> Reservations { get; set; } = [];
    }
}
