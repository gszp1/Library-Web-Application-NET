using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.dto
{
    public class AdminReservationDto
    {
        public int? ReservationId { get; set; }

        public string? UserEmail { get; set; }

        public int? InstanceId { get; set; }

        public string? Title { get; set; }

        public DateOnly? Start {  get; set; }

        public DateOnly? End { get; set; }

        public int? NumberOfExtensions { get; set; }

        public ReservationStatus? Status { get; set; }
    }
}
