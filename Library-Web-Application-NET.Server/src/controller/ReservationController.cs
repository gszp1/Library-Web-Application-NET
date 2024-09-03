using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet("all")]
        public async Task<ActionResult<List<AdminReservationDto>>> GetAllReservations()
        {
            return Ok(await reservationService.GetAllReservationsAsync());
        }

        [Authorize(Policy = "UserCreate")]
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateReservation([FromBody] ReservationRequest request)
        {
            try
            {
                await reservationService.CreateReservationAsync(request.UserEmail, request.InstanceId);
                return Ok("Reservation created.");
            }
            catch (InstanceReservedException ire)
            {
                return Conflict("Instance is already reserved.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (UserAlreadyReservedResourceException uarre)
            {
                return Conflict("You have already reserved such instance.");
            }
        }

        [Authorize(Policy = "UserRead")]
        [HttpPut("{email}/all")]
        public async Task<ActionResult<List<UserReservationDto>>> GetAllReservationsByUserEmail(string email)
        {
            return Ok(await reservationService.GetUserReservationsAsync(email));
        }

        [Authorize(Policy = "UserUpdate")]
        [HttpPut("{id}/extend")]
        public async Task<ActionResult<string>> ExtendReservation(int id)
        {
            try
            {
                await reservationService.ExtendReservationAsync(id);
                return Ok("Reservation extended.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }

        [Authorize(Policy = "UserUpdate")]
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<string>> CancelReservation(int id)
        {
            try
            {
                await reservationService.CancelReservationAsync(id);
                return Ok("Reservation cancelled");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpPut("{id}/borrow")]
        public async Task<ActionResult<string>> BorrowReservation(int id)
        {
            try
            {
                await reservationService.ChangeToBorrowAsync(id);
                return Ok("Resource borrowed.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateReservation([FromBody] AdminReservationDto dto) 
        { 
            try
            {
                await reservationService.UpdateReservationAsync(dto);
                return Ok("Reservation updated.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }
    }
}
