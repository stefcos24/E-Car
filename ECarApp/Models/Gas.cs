using System.ComponentModel.DataAnnotations;

namespace ECarApp.Models
{
    public class Gas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Type of Gas")]
        public string TypeOfGas { get; set; }
    }
}
