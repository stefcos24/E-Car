using System.ComponentModel.DataAnnotations;

namespace ECarApp.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Car Location")]
        public string CarLocation { get; set; }
    }
}
