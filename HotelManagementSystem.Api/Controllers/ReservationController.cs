using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Reservations")]
    public class ReservationController(IReservationService reservationService) : Controller
    {
        private readonly IReservationService _reservationService = reservationService;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            return Ok(await _reservationService.CreateAsync(reservation));
        }

        [HttpGet]
        [Route("{reservationId}")]
        public async Task<IActionResult> Get(int reservationId)
        {
            return Ok(await _reservationService.GetAsync(reservationId));
        }

        [HttpPost]
        [Route("{reservationId}/Remove")]
        public async Task<IActionResult> Remove(int reservationId)
        {
            await _reservationService.RemoveAsync(reservationId);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Reservation reservation)
        {
            return Ok(await _reservationService.UpdateAsync(reservation));
        }
    }
}
