namespace HotelManagementSystem.Interfaces.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";

        public const string User = "User";

        public readonly static IEnumerable<string> All =
        [
            Admin,
            User
        ];
    }
}
