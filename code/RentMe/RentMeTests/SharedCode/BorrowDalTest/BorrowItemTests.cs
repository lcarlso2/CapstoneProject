using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.SharedCode.BorrowDalTest
{
	/// <summary>
	/// The test class for borrow item 
	/// </summary>
	[TestClass()]
	public class BorrowItemTests
	{
		[TestMethod()]
		public void BorrowItemValidTest()
		{
			var borrowDal = new BorrowDal();
			var customer = new Customer
			{
				Email = "email@email.com",
				Password = "password"
			};

			var media = new Media();

		}

	}
}
