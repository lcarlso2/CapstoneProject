using System;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{

	public class RegisteringCustomer
	{

		[Required]
		[Display(Name = "First Name")]
		public string First { get; set; }

		
		[Required]
		[Display(Name = "Last Name")]
		public string Last { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Confirm Email")]
		[DataType(DataType.EmailAddress)]
		[Compare("Email")]
		public string ConfirmEmail { get; set; }


		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string State { get; set; }

	
		[RegularExpression(@"\b\d{5}\b", ErrorMessage = "Must be five digits")]
		[Required]
		public string Zip { get; set; }


	}

}