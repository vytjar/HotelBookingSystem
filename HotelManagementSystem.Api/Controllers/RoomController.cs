using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
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

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove(int roomId)
        {
            await _roomService.RemoveAsync(roomId);

            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Room room)
        {
            return Ok(await _roomService.UpdateAsync(room));
        }
    }
}
