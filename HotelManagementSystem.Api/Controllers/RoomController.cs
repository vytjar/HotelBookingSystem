using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Rooms")]
    public class RoomController(IRoomService roomService) : Controller
    {
        private readonly IRoomService _roomService = roomService;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Room room)
        {
            return Ok(await _roomService.CreateAsync(room));
        }

        [HttpGet]
        [Route("{roomId}")]
        public async Task<IActionResult> Get(int roomId)
        {
            return Ok(await _roomService.GetAsync(roomId));
        }

        [HttpGet]
        [Route("{roomId}/Reservations")]
        public async Task<IActionResult> GetReservations(int roomId)
        {
            return Ok(await _roomService.GetReservations(roomId));
        }

        [HttpPost]
        [Route("{roomId}/Remove")]
        public async Task<IActionResult> Remove(int roomId)
        {
            await _roomService.RemoveAsync(roomId);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Room room)
        {
            return Ok(await _roomService.UpdateAsync(room));
        }
    }
}
