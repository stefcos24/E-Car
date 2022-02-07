using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECar.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        [ValidateNever]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public int ConditionId { get; set; }
        [ForeignKey("ConditionId")]
        [ValidateNever]
        public Condition Condition { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }

        [Required]
        [Range(1900,2022,ErrorMessage = "Year must be between 1900 and 2022!")]
        public int Year { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "Mileage must be between 0 and 999999!")]
        public int Mileage { get; set; }

        [Range(0, 2000, ErrorMessage = "Kilowatt must be between 0 and 2000!")]
        public int Kilowatt { get; set; }

        [Required]
        [Display(Name = "Gas")]
        public int GasId { get; set; }
        [ForeignKey("GasId")]
        [ValidateNever]
        public Gas Gas { get; set; }

        [Required]
        [Display(Name = "Transimision")]
        public int TransimisionId { get; set; }
        [ForeignKey("TransimisionId")]
        [ValidateNever]
        public Transimision Transimision { get; set; }

        public string Description { get; set; }

        [DisplayName("Date Published")]
        public DateTime DatePublished { get; set; } = DateTime.Now;

        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
