using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.Controllers.RentMeController
{
	/// <summary>
	/// The mock customer dal for testing purposes
	/// </summary>
	public class MockCustomerDal : ICustomerDal
	{
		/// <summary>
		/// The authenticate value to return for testing
		/// </summary>

		public int AuthenticateValueToReturn { get; set; }

		/// <summary>
		/// Set to true when an error is needed
		/// </summary>
		public bool ThrowError { get; set; }
		public int Authenticate(string email, string password)
		{
			return AuthenticateValueToReturn;
		}


		public void RegisterCustomer(RegisteringCustomer customer)
		{
			if (ThrowError)
			{
				throw new Exception();
			}
		
		}
	}
}
