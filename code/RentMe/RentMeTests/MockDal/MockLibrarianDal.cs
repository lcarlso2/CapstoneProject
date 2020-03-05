using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.MockDal
{
	/// <summary>
	/// The mock librarian dal for testing purposes
	/// </summary>
	public class MockLibrarianDal : ILibrarianDal
	{
		public int AuthenticateValueToReturn { get; set; }

		public bool ThrowError { get; set; }

		public int Authenticate(string email, string password)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return AuthenticateValueToReturn;
		}

		public List<RegisteringMember> GetAllMembers()
		{
			var members = new List<RegisteringMember>();

			if (this.ThrowError)
			{
				throw new Exception();
			}

			return members;
		}
	}
}
