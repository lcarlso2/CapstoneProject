using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.Model;

namespace SharedCode.DAL
{
	/// <summary>
	/// The interface for all rental database interactions
	/// </summary>
	public interface IRentalDal
	{
		/// <summary>
		/// Retrieves all borrowed items.
		/// </summary>
		/// <returns>All borrowed items </returns>
		List<RentalItem> RetrieveAllRentedItems();

		/// <summary>
		/// Retrieves all rentals by customer email.
		/// </summary>
		/// <param name="email"> The email of the customer</param>
		/// <returns>All rentals by the customer </returns>
		List<RentalItem> RetrieveAllRentalsByCustomer(string email);

		/// <summary>
		/// Retrieves select borrowed items with the given status.
		/// </summary>
		/// <param name="selectedStatus"> the desired status</param>
		/// <returns>select borrowed items with the given status </returns>
		List<RentalItem> RetrieveSelectRentedItems(string selectedStatus);

		/// <summary>
		/// Updates the status of the given rental
		/// </summary>
		/// <param name="transactionId">the rental transaction id</param>
		/// <param name="status"> the status </param>
		/// <param name="employeeId">the employee updating the rental</param>
		/// <param name="condition">the condition of the rental</param>
		/// <returns>the rows affected</returns>
		int UpdateStatus(int transactionId, string status, int employeeId, string condition);

		/// <summary>
		/// Gets the history for a specific rental transaction id
		/// </summary>
		/// <param name="rentalId"> the desired rental id</param>
		/// <returns>the history for the specific rental transaction or an error if something went wrong on the DB</returns>
		List<RentalItem> RetrieveHistoryForRentalTransaction(int rentalId);
	}
}
