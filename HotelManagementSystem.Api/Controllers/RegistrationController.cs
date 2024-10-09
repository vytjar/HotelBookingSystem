using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [Route("Api/Registrations")]
    public class RegistrationController(IRegistrationService registrationService) : Controller
    {
        private readonly IRegistrationService _registrationService = registrationService;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Registration registration)
        {
            return Ok(await _registrationService.CreateAsync(registration));
        }

        [HttpGet]
        [Route("{registrationId}")]
        public async Task<IActionResult> Get(int registrationId)
        {
            return Ok(await _registrationService.GetAsync(registrationId));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove(int registrationId)
        {
            await _registrationService.RemoveAsync(registrationId);

            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Registration registration)
        {
            return Ok(await _registrationService.UpdateAsync(registration));
        }
    }
}
