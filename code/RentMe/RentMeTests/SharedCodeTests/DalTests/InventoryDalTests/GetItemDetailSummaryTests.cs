using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.InventoryDalTests
{
	/// <summary>
	/// The test class for get item detail summary
	/// </summary>
	[TestClass()]
	public class GetItemDetailSummaryTests
	{
		[TestMethod()]
		public void GetItemDetailSummary()
		{
			var inventoryDal = new InventoryDal();

			var result = inventoryDal.GetItemHistorySummary(1);

			Assert.AreEqual(3, result.Count);
		}
	}
}
