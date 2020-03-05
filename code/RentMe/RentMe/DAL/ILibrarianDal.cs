using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentMe.Models;
using SharedCode.Model;

namespace RentMe.DAL
{
	public interface ILibrarianDal
	{
		/// <summary>
		/// Authenticates the specified librarian.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="password">The password.</param>
		/// <returns>1 if the librarian is valid otherwise 0</returns>
		int Authenticate(string email, string password);

		/// <summary>
		/// Gets all the customers from the db
		/// </summary>
		/// <returns>all the customers from the db or an error if something went wrong</returns>
		List<RegisteringMember> GetAllMembers();

	}
}
