using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Interfaces.Dto.Requests
{
    public class LoginRequest
    {
        [StringLength(100)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [StringLength(256)]
        public required string UserName { get; set; }
    }
}
