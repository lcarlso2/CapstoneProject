using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.RentalDalTests
{
	/// <summary>
	/// The test class for retrieve all rented items by customer 
	/// </summary>
	[TestClass()]
	public class RetrieveAllRentalsByCustomerTests
	{
		[TestMethod()]
		public void RetrieveAllRentedItemsByGivenCustomerTest()
		{
			var rentalDal = new RentalDal();
			var result = rentalDal.RetrieveAllRentalsByCustomer("lcarlso2@my.westga.edu");

			Assert.AreEqual(4, result.Count);

		}
	}
}
