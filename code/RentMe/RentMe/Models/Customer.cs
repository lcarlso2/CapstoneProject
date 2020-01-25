using System;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{

	public class Customer
	{

		[Required]
		public string FName { get; set; }

		
		[Required]
		public string LName { get; set; }

		[Required]
		public string Email { get; set; }


		[Required]
		public string Password { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string State { get; set; }

	
		[RegularExpression(@"\b\d{5}\b", ErrorMessage = "Must be five digits")]
		[Required]
		public string Zip { get; set; }


	}

}