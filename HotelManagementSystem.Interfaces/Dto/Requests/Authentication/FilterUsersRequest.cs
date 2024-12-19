namespace HotelManagementSystem.Interfaces.Dto.Requests.Authentication
{
    public class FilterUsersRequest
    {
        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }

        public IEnumerable<string> Roles { get; set; } = [];
    }
}
