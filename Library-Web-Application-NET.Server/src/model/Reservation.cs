﻿using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.model
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateOnly ReservationStart { get; set; }

        public DateOnly ReservationEnd { get; set; }
            
        public ReservationStatus Status { get; set; }

        public int ExtensionCount { get; set; }
    }
}
