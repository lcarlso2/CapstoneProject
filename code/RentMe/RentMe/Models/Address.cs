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

		[Required]
		[Range(1, int.MaxValue)]
		public int AddressId { get; set; }

		/// <summary>
		/// Gets or sets the password
		/// </summary>
		[Required]
		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }

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

		public Address()
		{
			this.AddressId = DEFAULT_ID;
		}
	}
}
