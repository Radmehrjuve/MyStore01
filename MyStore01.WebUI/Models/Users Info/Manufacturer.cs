using System.ComponentModel.DataAnnotations;
namespace MyStore01.WebUI.Models.Users_Info
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManufactureEmail { get; set; } = string.Empty;
        public string ManufacturePhone { get; set; } = string.Empty;
    }
}
