using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Template.Models.DTOs
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
