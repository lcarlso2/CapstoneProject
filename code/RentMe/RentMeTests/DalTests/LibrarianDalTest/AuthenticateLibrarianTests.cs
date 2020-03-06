using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.LibrarianDalTest
{
	/// <summary>
	/// The test class for authenticating the customer
	/// </summary>
	[TestClass()]
	public class AuthenticateLibrarianTests
	{

		[TestMethod()]
		public void AuthenticateValidTest()
		{
			var librarianDal = new LibrarianDal();

			var result = librarianDal.Authenticate("librarian@email.com", "1234");

			Assert.AreEqual(1, result);
		}

		[TestMethod()]
		public void AuthenticateInValidEmailTest()
		{
			var librarianDal = new LibrarianDal();

			var result = librarianDal.Authenticate("invalid@email.com", "password");

			Assert.AreEqual(0, result);
		}

		[TestMethod()]
		public void AuthenticateInValidPasswordTest()
		{
			var librarianDal = new LibrarianDal();

			var result = librarianDal.Authenticate("email@email.com", "nope");

			Assert.AreEqual(0, result);
		}
	}
}
