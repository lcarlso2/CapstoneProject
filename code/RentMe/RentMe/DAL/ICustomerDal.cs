using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentMe.Models;

namespace RentMe.DAL
{
	/// <summary>
	/// The Dal interface for the customer object 
	/// </summary>
	public interface ICustomerDal
	{
		/// <summary>
		/// Authenticates the specified customer.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="password">The password.</param>
		/// <returns>1 if the customer is valid otherwise 0</returns>
		int Authenticate(string email, string password);

		/// <summary>
		/// Registers the customer.
		/// </summary>
		/// <param name="customer">The customer to register.</param>
		/// @precondition none
		/// @postcondition the customer is added to the db
		void RegisterCustomer(RegisteringCustomer customer);

		/// <summary>
		/// Adds an address to the db for the given customer 
		/// </summary>
		/// <param name="address"> the address being added</param>
		/// <param name="customer"> the customer the address is being added to</param>
		/// @precondition none
		/// @postcondition the address is added to the db
		void AddAddress(Address address, Customer customer);

		/// <summary>
		/// Gets the addresses stored for the given customer 
		/// </summary>
		/// <param name="customer"> the customer the addresses are being gotten for</param>
		/// <returns>the addresses for the given customer</returns>
		List<Address> GetAddresses(Customer customer);

		/// <summary>
		/// Updates the customers email that has a matching original email
		/// </summary>
		/// <param name="originalEmail"> The current logged in members email</param>
		/// <param name="updatedEmail"> The email to update for the customer</param>
		/// @precondition none
		/// @postcondition the email is updated
		void UpdateEmail(string originalEmail, string updatedEmail);

	}
}
