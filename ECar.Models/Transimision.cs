using System.ComponentModel.DataAnnotations;

namespace ECar.Models
{
    public class Transimision
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Transimision Type")]
        public string TransimisionType { get; set; }
    }
}
