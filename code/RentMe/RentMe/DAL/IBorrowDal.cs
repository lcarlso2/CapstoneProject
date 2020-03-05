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
	public interface IBorrowDal
	{
		/// <summary>
		/// Borrows an item 
		/// </summary>
		/// <param name="member"> the member borrowing an item</param>
		/// <param name="media"> the media being rented</param>
		/// <param name="addressId"> the id of the address the item is being shipped to </param>
		/// <returns>the number of rows altered</returns>
		int BorrowItem(Member member, Media media, int addressId);

		/// <summary>
		/// Gets the number of open rentals a member has
		/// </summary>
		/// <param name="member">the member being checked </param>
		/// <returns>the number of open rentals or an error if something goes wrong </returns>
		int GetNumberOfOpenRentals(Member member);
	}
}
