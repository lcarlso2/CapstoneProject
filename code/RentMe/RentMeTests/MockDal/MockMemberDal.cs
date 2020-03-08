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
	public class MockMemberDal : IMemberDal
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


		public void RegisterMember(RegisteringMember member)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		
		}

		public void AddAddress(Address address, Member member)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}

		public List<Address> GetAddresses(Member member)
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

        public void RemoveAddress(string address, string email)
        {
            if (this.ThrowError)
            {
                throw new Exception();
            }
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


		public List<RegisteringMember> GetOverdueMembers()
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
