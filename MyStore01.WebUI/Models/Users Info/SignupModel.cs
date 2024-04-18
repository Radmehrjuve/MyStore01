using System.ComponentModel.DataAnnotations;

namespace MyStore01.WebUI.Models.Users_Info
{
    public class SignupModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
