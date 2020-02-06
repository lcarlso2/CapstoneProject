using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.Model;

namespace RentMeTests.SharedCode
{
	[TestClass()]
	public class RentalItemTests
	{
		[TestMethod()]
		public void TestCreateRentalItemConstructor()
		{
			var item = new RentalItem();

			Assert.AreEqual(null, item.Category);
			Assert.AreEqual(null, item.MemberEmail);
			Assert.AreEqual(new DateTime(), item.RentalDate);
			Assert.AreEqual(new DateTime(), item.ReturnDate);
			Assert.AreEqual(null, item.Status);
			Assert.AreEqual(null, item.Title);
		}

		[TestMethod()]
		public void TestGetPossibleStatusesWithOrdered()
		{
			var expected = new List<string> {"Ordered", "Shipped"};
			var actual = RentalItem.GetPossibleStatuses("Ordered");
			Assert.AreEqual(expected.Count, actual.Count);
			Assert.AreEqual(expected[0], actual[0]);
			Assert.AreEqual(expected[1], actual[1]);
		}

		[TestMethod()]
		public void TestGetPossibleStatusesWithShipped()
		{
			var expected = new List<string> { "Shipped", "Returned" };
			var actual = RentalItem.GetPossibleStatuses("Shipped");
			Assert.AreEqual(expected.Count, actual.Count);
			Assert.AreEqual(expected[0], actual[0]);
			Assert.AreEqual(expected[1], actual[1]);
		}

		[TestMethod()]
		public void TestGetPossibleStatusesWithReturned()
		{
			var expected = new List<string> { "Returned" };
			var actual = RentalItem.GetPossibleStatuses("Returned");
			Assert.AreEqual(expected.Count, actual.Count);
			Assert.AreEqual(expected[0], actual[0]);
		}

	}
}
