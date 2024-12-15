namespace HotelManagementSystem.Interfaces.Dto.Requests.Authentication
{
    public class AssignRolesRequest
    {
        public IEnumerable<string> Roles { get; set; } = [];

        public required string UserName { get; set; }
    }
}
