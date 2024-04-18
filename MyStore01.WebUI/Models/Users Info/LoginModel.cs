using System.ComponentModel.DataAnnotations;

namespace MyStore01.WebUI.Models.Users_Info
{
    public class LoginModel
    {
        [Required]
        [UIHint("Email")]
        public string Email { get; set; } 
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
