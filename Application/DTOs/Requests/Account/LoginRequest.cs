using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Request.Login
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
