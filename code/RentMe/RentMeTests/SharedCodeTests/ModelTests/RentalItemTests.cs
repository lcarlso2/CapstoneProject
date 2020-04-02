using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.Model;

namespace RentMeTests.SharedCodeTests.ModelTests
{
	/// <summary>
	/// The test class for the rental item
	/// </summary>
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
		public void TestCreateRentalItemSetters()
		{
			var item = new RentalItem
			{
				MemberId = 1,
				RentalId = 1,
				InventoryId = 1,
				MemberEmail = "email",
				RentalDate = new DateTime(),
				ReturnDate = new DateTime(),
				Category = "category",
				Title = "title",
				Status = "Ordered"
			};

			Assert.AreEqual(1, item.MemberId);
			Assert.AreEqual(1, item.RentalId);
			Assert.AreEqual(new DateTime(), item.RentalDate);
			Assert.AreEqual(new DateTime(), item.ReturnDate);
			Assert.AreEqual(1, item.InventoryId);
			Assert.AreEqual("email", item.MemberEmail);
			Assert.AreEqual("category", item.Category);
			Assert.AreEqual("title", item.Title);
			Assert.AreEqual("Ordered", item.Status);
		}

		[TestMethod()]
		public void TestCreateRentalItemInfo()
		{
			var item = new RentalItem
			{
				MemberId = 1,
				RentalId = 1,
				InventoryId = 1,
				MemberEmail = "email",
				RentalDate = new DateTime(),
				ReturnDate = new DateTime(),
				Category = "category",
				Title = "title",
				Status = "Ordered"
			};

			Assert.AreEqual($"Rental ID: {item.RentalId}, Inventory ID: {item.InventoryId}, Category: {item.Category}, Title: {item.Title}\nMember ID: {item.MemberId} Member Email: {item.MemberEmail}\nRental Date: {item.RentalDate}, Due Date: {item.ReturnDate}, Status: {item.Status}\n", item.RentalItemInfo);
			
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

		[TestMethod()]
		public void TestGetPossibleStatusIdWithOrdered()
		{
			Assert.AreEqual(1, RentalItem.GetStatusId("Ordered"));
		}

		[TestMethod()]
		public void TestGetPossibleStatusIdWithShipped()
		{
			Assert.AreEqual(2, RentalItem.GetStatusId("Shipped"));
		}

		[TestMethod()]
		public void TestGetPossibleStatusIdWithReturned()
		{
			Assert.AreEqual(4, RentalItem.GetStatusId("Returned"));
		}

	}
}
