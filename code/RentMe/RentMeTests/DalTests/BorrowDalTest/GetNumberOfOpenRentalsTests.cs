using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.DalTests.BorrowDalTest
{
	/// <summary>
	/// The test class for get number of open rentals 
	/// </summary>
	[TestClass()]
	public class GetNumberOfOpenRentalsTests
	{
		[TestMethod()]
		public void GetNumberOfOpenRentalsTest()
		{
			var borrowDal = new BorrowDal();

			var result = borrowDal.GetNumberOfOpenRentals(new Customer {Email = "email@email.com"});
			Assert.AreEqual(3, result);
		}
	}
}
