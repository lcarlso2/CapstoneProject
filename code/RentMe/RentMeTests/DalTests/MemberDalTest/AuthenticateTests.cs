using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.CustomerDalTest
{
	/// <summary>
	/// The test class for authenticating the customer
	/// </summary>
	[TestClass()]
	public class AuthenticateTests
	{

		[TestMethod()]
		public void AuthenticateValidTest()
		{
			var customerDal = new MemberDal();

			var result = customerDal.Authenticate("email@email.com", "password");

			Assert.AreEqual(1, result);
		}

		[TestMethod()]
		public void AuthenticateInValidEmailTest()
		{
			var customerDal = new MemberDal();

			var result = customerDal.Authenticate("invalid@email.com", "password");

			Assert.AreEqual(0, result);
		}

		[TestMethod()]
		public void AuthenticateInValidPasswordTest()
		{
			var customerDal = new MemberDal();

			var result = customerDal.Authenticate("email@email.com", "nope");

			Assert.AreEqual(0, result);
		}
	}
}
