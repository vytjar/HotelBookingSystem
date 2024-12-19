using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Rooms")]
    public class RoomController(IRoomService roomService) : Controller
    {
        private readonly IRoomService _roomService = roomService;

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">A room to be created.</param>
        /// <returns>The created room.</returns>
        /// <response code="201">The room was successfully created.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided room data is semantically invalid.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created(string.Empty, await _roomService.CreateAsync(room));
        }

        /// <summary>
        /// Deletes a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to delete.</param>
        /// <response code="204">The room was successfully deleted.</response>
        /// <response code="404">If the room with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        [Route("{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int roomId)
        {
            await _roomService.DeleteAsync(roomId);

            return NoContent();
        }

        /// <summary>
        /// Retrieves a room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <returns>The room with the specified ID.</returns>
        /// <response code="200">The room was found and returned.</response>
        /// <response code="404">If the room with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet]
        [Route("{roomId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoom(int roomId)
        {
            return Ok(await _roomService.GetRoomAsync(roomId));
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of rooms.</returns>
        /// <response code="200">The rooms were found and returned.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await _roomService.GetRoomsAsync());
        }

        /// <summary>
        /// Retrieves all reservations of a room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <returns>A list of reservations for the room.</returns>
        /// <response code="200">The reservations were found and returned.</response>
        /// <response code="404">If the room with the given ID was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet]
        [Route("{roomId}/Reservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReservations(int roomId)
        {
            return Ok(await _roomService.GetReservations(roomId));
        }

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="room">The updated room data.</param>
        /// <returns>The updated room.</returns>
        /// <response code="200">The room was successfully updated.</response>
        /// <response code="400">If the request is malformed.</response>
        /// <response code="422">If the provided room data is semantically invalid.</response>
        /// <response code="404">If the room to be updated was not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _roomService.UpdateAsync(room));
        }
    }
}
