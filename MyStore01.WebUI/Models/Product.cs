using System.ComponentModel.DataAnnotations;

namespace MyStore01.WebUI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public string IsAvailable { get; set; }
    }
}
