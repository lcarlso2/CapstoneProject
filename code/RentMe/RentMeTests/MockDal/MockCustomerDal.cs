using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

		public void AddAddress(Address address, Customer customer)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}

		public List<Address> GetAddresses(Customer customer)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}

			return new List<Address>();
		}
		public void UpdateEmail(string originalEmail, string updatedEmail)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}

	}
}
