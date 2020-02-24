using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of the registering customer
	/// </summary>
	public class RegisteringCustomer : Customer
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
		/// Gets or sets the confirm email
		/// </summary>
		[Required]
		[Display(Name = "Confirm Email")]
		[DataType(DataType.EmailAddress)]
		[Compare("Email")]
		public string ConfirmEmail { get; set; }

		/// <summary>
		/// Gets or sets the confirm password
		/// </summary>
		[Required]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }


		/// <summary>
		/// Gets or sets the Address
		/// </summary>
		public Address Address { get; set; }
	}
}
