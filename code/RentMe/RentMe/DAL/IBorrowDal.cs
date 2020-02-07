using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentMe.Models;

namespace RentMe.DAL
{
	/// <summary>
	/// The dal interface borrowing items
	/// </summary>
	interface IBorrowDal
	{
		/// <summary>
		/// Borrows an item 
		/// </summary>
		/// <param name="customer"> the customer borrowing an item</param>
		/// <param name="media"> the media being rented</param>
		void BorrowItem(Customer customer, Media media);
	}
}
