using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of the librarian object
	/// </summary>
	public class Librarian 
	{
		/// <summary>
		/// Gets or sets the email of the librarian
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
