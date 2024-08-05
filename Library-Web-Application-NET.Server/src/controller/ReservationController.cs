using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService reservationService;

        public ReservationController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("all")]
        public ActionResult<List<AdminReservationDto>> GetAllReservations()
        {

        }

        [HttpPost("create")]
        public ActionResult<string> CreateReservation([FromBody] ReservationRequest request)
        {

        }

        [HttpPut("{email}/all")]
        public ActionResult<List<UserReservationDto>> GetAllReservationsByUserEmail(string email)
        {

        }

        [HttpPut("{id}/extend")]
        public ActionResult<string> ExtendReservation(int id)
        {

        }

        [HttpPut("{id}/cancel")]
        public ActionResult<string> CancelReservation(int id)
        {

        }

        [HttpPut("{id}/borrow")]
        public ActionResult<string> BorrowReservation(int id)
        {

        }

        [HttpPut("update")]
        public ActionResult<string> UpdateReservation([FromBody] AdminReservationDto dto) 
        { 

        }
    }
}
