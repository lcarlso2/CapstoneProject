using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.RentalDalTests
{
	/// <summary>
	/// The test class for retrieve all rented items 
	/// </summary>
	[TestClass()]
	public class RetrieveAllRentedItemsTest
	{

		[TestMethod()]
		public void RetrieveAllRentedItemsValidTest()
		{
			var rentalDal = new RentalDal();

			var result = rentalDal.RetrieveAllRentedItems();

			Assert.AreEqual(4, result.Count);
		}
	}
}
