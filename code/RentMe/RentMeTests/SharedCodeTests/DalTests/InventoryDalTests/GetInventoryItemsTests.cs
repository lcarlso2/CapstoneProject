using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.InventoryDalTests
{
	/// <summary>
	/// The test class for get inventory items 
	/// </summary>
	[TestClass()]
	public class GetInventoryItemsTests
	{
		[TestMethod()]
		public void GetInventoryItemsTest()
		{
			var inventoryDal = new InventoryDal();
			var result = inventoryDal.GetInventoryItems();

			Assert.AreEqual(23, result.Count);
		}
	}
}
