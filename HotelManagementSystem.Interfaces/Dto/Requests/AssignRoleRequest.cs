namespace HotelManagementSystem.Interfaces.Dto.Requests
{
    public class AssignRoleRequest
    {
        public required string Role { get; set; }

        public required string UserName { get; set; }
    }
}
