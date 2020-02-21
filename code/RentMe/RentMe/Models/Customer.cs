using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public List<Address> Addresses { get; set; }

		public Customer()
		{
			this.Addresses = new List<Address>{new Address{State = "GA", StreetAddress = "7801 HWY 166", Zip = "30135", AddressId = 1}};
		}

	}
}
