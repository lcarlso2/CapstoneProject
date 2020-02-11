using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.RentalDalTests
{
	/// <summary>
	/// The test class for retrieve rentals with selected status 
	/// </summary>
	[TestClass()]
	public class RetrieveSelectRentedItemsTests
	{
		[TestMethod()]
		public void RetrieveAllRentedItemsWithDeliveredForStatusTest()
		{
			var rentalDal = new RentalDal();
			var result = rentalDal.RetrieveSelectRentedItems("Delivered");

			Assert.AreEqual(0, result.Count);
		}
	}
}
