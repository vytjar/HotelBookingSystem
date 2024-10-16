using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Hotels")]
    public class HotelController(IHotelService hotelService) : Controller
    {
        private readonly IHotelService _hotelService = hotelService;

        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">A hotel to be created.</param>
        /// <returns>The created hotel.</returns>
        /// <response code="201">The hotel was successfully created.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided hotel data is semantically invalid.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created(string.Empty, await _hotelService.CreateAsync(hotel));
        }

        /// <summary>
        /// Deletes a hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to delete.</param>
        /// <response code="204">The hotel was successfully deleted.</response>
        /// <response code="404">If the hotel with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpDelete]
        [Route("{hotelId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int hotelId)
        {
            await _hotelService.DeleteAsync(hotelId);

            return NoContent();
        }

        /// <summary>
        /// Retrieves a hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <returns>The hotel with the specified ID.</returns>
        /// <response code="200">The hotel was found and returned.</response>
        /// <response code="404">If the hotel with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet]
        [Route("{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int hotelId)
        {
            return Ok(await _hotelService.GetHotelAsync(hotelId));
        }

        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>A list of hotels.</returns>
        /// <response code="200">The hotels were found and returned.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            return Ok(await _hotelService.GetHotelsAsync());
        }

        /// <summary>
        /// Retrieves all rooms of a hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <returns>A list of reservations for the hotel.</returns>
        /// <response code="200">The rooms were found and returned.</response>
        /// <response code="404">If the room with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet]
        [Route("{hotelId}/Rooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRooms(int hotelId)
        {
            return Ok(await _hotelService.GetRoomsAsync(hotelId));
        }

        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="hotel">The updated hotel data.</param>
        /// <returns>The updated hotel.</returns>
        /// <response code="200">The hotel was successfully updated.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided hotel data is semantically invalid.</response>
        /// <response code="404">If the hotel to be updated was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _hotelService.UpdateAsync(hotel));
        }
    }
}
