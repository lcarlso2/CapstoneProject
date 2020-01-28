﻿
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of customer object
	/// </summary>
	public class Customer
	{
		/// <summary>
		/// Gets or sets the email of the customer
		/// </summary>
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		/// <summary>
		/// Gets or set the password
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
