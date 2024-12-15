using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Reservations")]
    public class ReservationController(IReservationService reservationService) : Controller
    {
        private readonly IReservationService _reservationService = reservationService;

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="reservation">A reservation to be created.</param>
        /// <returns>The created reservation.</returns>
        /// <response code="201">The reservation was successfully created.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided reservation data is semantically invalid.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            reservation.UserId = userId;

            return Created(string.Empty, await _reservationService.CreateAsync(reservation));
        }

        /// <summary>
        /// Deletes a reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to delete.</param>
        /// <response code="204">The reservation was successfully deleted.</response>
        /// <response code="404">If the reservation with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize]
        [HttpDelete]
        [Route("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int reservationId)
        {
            var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            await _reservationService.DeleteAsync(reservationId, userId);

            return NoContent();
        }

        /// <summary>
        /// Filters reservations.
        /// </summary>
        /// <param name="filterReservationsRequest">The filter request.</param>
        /// <returns>A list of reservations.</returns>
        /// <response code="200">The reservations were found and returned.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost]
        [Route("Filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Filter(FilterReservationsRequest filterReservationsRequest)
        {
            return Ok(await _reservationService.FilterAsync(filterReservationsRequest));
        }

        /// <summary>
        /// Retrieves a reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation.</param>
        /// <returns>The reservation with the specified ID.</returns>
        /// <response code="200">The reservation was found and returned.</response>
        /// <response code="404">If the reservation with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize]
        [HttpGet]
        [Route("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReservation(int reservationId)
        {
            var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            return Ok(await _reservationService.GetReservationAsync(reservationId, userId));
        }

        /// <summary>
        /// Retrieves a list of all reservations.
        /// </summary>
        /// <returns>A list of reservations.</returns>
        /// <response code="200">The reservations were found and returned.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReservations()
        {
            return Ok(await _reservationService.GetReservationsAsync());
        }

        /// <summary>
        /// Updates an existing reservation.
        /// </summary>
        /// <param name="reservation">The updated reservation data.</param>
        /// <returns>The updated reservation.</returns>
        /// <response code="200">The reservation was successfully updated.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided reservation data is semantically invalid.</response>
        /// <response code="404">If the reservation to be updated was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize]
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            return Ok(await _reservationService.UpdateAsync(reservation, userId));
        }
    }
}
