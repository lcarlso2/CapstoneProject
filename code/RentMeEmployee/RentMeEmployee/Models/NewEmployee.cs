using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentMeEmployee.Models
{
	public class NewEmployee : Employee
	{
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LName { get; set; }
    }
}
