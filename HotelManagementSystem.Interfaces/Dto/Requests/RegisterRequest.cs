using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Interfaces.Dto.Requests
{
    public class RegisterRequest
    {
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one symbol.")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string PasswordConfirmation { get; set; }

        [StringLength(256, ErrorMessage = "{0} cannot exceed {1} characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(50)]
        public required string Surname { get; set; }

        [StringLength(256)]
        public required string UserName { get; set; }
    }
}
