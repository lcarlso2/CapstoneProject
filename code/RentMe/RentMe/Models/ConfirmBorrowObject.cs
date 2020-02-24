using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Models
{
	/// <summary>
	/// The confirm borrow class responsible for keeping track of the confirm borrow object
	/// </summary>
	public class ConfirmBorrowObject
	{

		/// <summary>
		/// Gets or sets the address id the item is being shipped to 
		/// </summary>
		/// @precondition none
		/// @postcondition the id is set
		[Required]
		[Range(1, int.MaxValue)]
		public int AddressId { get; set; }



		
	}
}
