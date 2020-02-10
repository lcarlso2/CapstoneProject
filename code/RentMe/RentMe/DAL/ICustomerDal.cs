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
		void RegisterCustomer(RegisteringCustomer customer);

	}
}
