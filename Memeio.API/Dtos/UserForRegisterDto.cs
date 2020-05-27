using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Memeio.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?!.*[^a-zA-Z0-9@#$^+=])(.{8,})$", ErrorMessage="Requires 1 uppercase, 1 number, and 1 special character that is at least 8 characters")]
        public string Password { get; set; }
    }
}