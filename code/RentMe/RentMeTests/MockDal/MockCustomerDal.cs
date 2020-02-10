using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.MockDal
{
	/// <summary>
	/// The mock customer dal for testing purposes
	/// </summary>
	public class MockCustomerDal : ICustomerDal
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


		public void RegisterCustomer(RegisteringCustomer customer)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		
		}
	}
}
