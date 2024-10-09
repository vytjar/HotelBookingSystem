using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [Route("Api/Hotels")]
    public class HotelController(IHotelService hotelService) : Controller
    {
        private readonly IHotelService _hotelService = hotelService;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            return Ok(await _hotelService.CreateAsync(hotel));
        }

        [HttpGet]
        [Route("{hotelId}")]
        public async Task<IActionResult> Get(int hotelId)
        {
            return Ok(await _hotelService.GetAsync(hotelId));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove(int hotelId)
        {
            await _hotelService.RemoveAsync(hotelId);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Hotel hotel)
        {
            return Ok(await _hotelService.UpdateAsync(hotel));
        }
    }
}
