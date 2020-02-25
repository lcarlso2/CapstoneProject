using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RentMe.Controllers;

namespace RentMe.Models
{
	/// <summary>
	/// The address class responsible for keeping track of the address object
	/// </summary>
	public class Address
	{

		private const int DEFAULT_ID = 2;


		/// <summary>
		/// Gets or sets the address id 
		/// </summary>
		/// @precondition none
		/// @postcondition the address id is set
		[Required]
		[Range(1, int.MaxValue)]
		public int AddressId { get; set; }

		/// <summary>
		/// Gets or sets the password
		/// </summary>
		/// @precondition none
		/// @postcondition the street address is set 
		[Required]
		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }

		/// <summary>
		/// Gets or sets the state 
		/// </summary>
		/// @precondition none
		/// @postcondition the state is set 
		[Required]
		public string State { get; set; }


		/// <summary>
		/// Gets or sets the zip
		/// </summary>
		/// @precondition none
		/// @postcondition the zip is set
		[RegularExpression(@"\b\d{5}\b", ErrorMessage = "Must be five digits")]
		[Required]
		public string Zip { get; set; }

		/// <summary>
		/// Creates a new address object with the default id
		/// </summary>
		/// @precondition none
		/// @postcondition a new address object is created 
		public Address()
		{
			this.AddressId = DEFAULT_ID;
		}
	}
}
