using System.ComponentModel.DataAnnotations;

namespace MyStore01.WebUI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [UIHint("Name")]
        public string Name { get; set; }
        [Required]
        [UIHint("Date")]
        public DateTime ProduceDate { get; set; }
        [Required]
        [UIHint("Phone number")]
        public string ManufacturePhone { get; set; }
        [Required]
        [UIHint("Email")]
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
