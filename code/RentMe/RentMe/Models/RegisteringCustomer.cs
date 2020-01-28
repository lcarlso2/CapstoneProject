using System;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of the registering customer
	/// </summary>
	public class RegisteringCustomer
	{
		/// <summary>
		/// Gets or sets the first name
		/// </summary>
		[Required]
		[Display(Name = "First Name")]
		public string First { get; set; }

		
		/// <summary>
		/// Gets or sets the last name
		/// </summary>
		[Required]
		[Display(Name = "Last Name")]
		public string Last { get; set; }

		/// <summary>
		/// Gets or sets the email 
		/// </summary>
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the confirm email
		/// </summary>
		[Required]
		[Display(Name = "Confirm Email")]
		[DataType(DataType.EmailAddress)]
		[Compare("Email")]
		public string ConfirmEmail { get; set; }

		/// <summary>
		/// Gets or sets the password
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the confirm password
		/// </summary>
		[Required]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }


		/// <summary>
		/// Gets or sets the password
		/// </summary>
		[Required]
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the state 
		/// </summary>
		[Required]
		public string State { get; set; }

	
		/// <summary>
		/// Gets or sets the zip
		/// </summary>
		[RegularExpression(@"\b\d{5}\b", ErrorMessage = "Must be five digits")]
		[Required]
		public string Zip { get; set; }


	}

}