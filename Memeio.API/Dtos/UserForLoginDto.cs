using System.ComponentModel.DataAnnotations;

namespace Memeio.API.Dtos
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}