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
	public interface IMemberDal
	{
		/// <summary>
		/// Authenticates the specified member.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="password">The password.</param>
		/// <returns>1 if the member is valid otherwise 0</returns>
		int Authenticate(string email, string password);

		/// <summary>
		/// Registers the member.
		/// </summary>
		/// <param name="member">The member to register.</param>
		/// @precondition none
		/// @postcondition the member is added to the db
		void RegisterMember(RegisteringMember member);

		/// <summary>
		/// Adds an address to the db for the given member 
		/// </summary>
		/// <param name="address"> the address being added</param>
		/// <param name="member"> the member the address is being added to</param>
		/// @precondition none
		/// @postcondition the address is added to the db
		void AddAddress(Address address, Member member);

		/// <summary>
		/// Gets the addresses stored for the given member 
		/// </summary>
		/// <param name="member"> the member the addresses are being gotten for</param>
		/// <returns>the addresses for the given member</returns>
		List<Address> GetAddresses(Member member);

		/// <summary>
		/// Updates the customers email that has a matching original email
		/// </summary>
		/// <param name="originalEmail"> The current logged in members email</param>
		/// <param name="updatedEmail"> The email to update for the customer</param>
		/// @precondition none
		/// @postcondition the email is updated
		void UpdateEmail(string originalEmail, string updatedEmail);

        /// <summary>
        /// Removes the address from the users list of addresses
        /// </summary>
        /// <param name="address"> The address to remove</param>
        /// <param name="email"> The email of the current user logged in</param>
        /// @precondition none
        /// @postcondition the address is removed
        void RemoveAddress(string address, string email);

		/// <summary>
		/// Gets all the members from the db
		/// </summary>
		/// <returns>all the members from the db or an error if something went wrong</returns>
		List<RegisteringMember> GetAllMembers();


		/// <summary>
		/// Gets all members that have overdue rentals 
		/// </summary>
		/// <returns> all members that have overdue rentals or an error if something went wrong with thd DB</returns>
		List<RegisteringMember> GetOverdueMembers();
	}
}
