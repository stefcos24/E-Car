﻿using System.ComponentModel.DataAnnotations;

namespace ECarApp.Models
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Car Condition")]
        public string CarCondition { get; set; }
    }
}
