using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Models
{
	public class Librarian 
	{
		/// <summary>
		/// Gets or sets the email of the customer
		/// </summary>
		/// @precondition none
		/// @postcondition the email address is set 
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// Gets or set the password
		/// </summary>
		/// @precondition none
		/// @postcondition the address is set
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
