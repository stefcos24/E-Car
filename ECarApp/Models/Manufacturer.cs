using System.ComponentModel.DataAnnotations;

namespace ECarApp.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Year Founded")]
        [Range(1800, 2020, ErrorMessage = "Year must be between 1800 and 2020!")]
        public int YearFounded { get; set; }
    }
}
