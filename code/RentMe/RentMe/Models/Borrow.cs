using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Models
{
	/// <summary>
	/// The borrow class responsible for keeping track of the borrow object
	/// </summary>
	public class Borrow
	{

		/// <summary>
		/// Gets or sets the address the item is being shipped to 
		/// </summary>
		/// @precondition none
		/// @postcondition the item is set
		[Required]
		public Address ShippingAddress { get; set; }

		/// <summary>
		/// Gets or sets the item being borrowed 
		/// </summary>
		/// @precondition none
		/// @postcondition the item is set
		public Media MediaItem { get; set; }

		public Borrow()
		{
			this.ShippingAddress = new Address();
			this.MediaItem = new Media();
		}
	}
}
