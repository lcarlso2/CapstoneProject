using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using RentMe.DAL;
using RentMe.Models;
using RentMeTests.MockDal;
using SharedCode.Model;
using SharedCode.TestObjects;

namespace RentMeTests.Controllers
{
	/// <summary>
	/// The test class for the librarian controller of the RentMe project
	/// </summary>
	[TestClass()]
	public class LibrarianControllerTests
	{

		[TestMethod()]
		public void TestDefaultConstructor()
		{
			var controller = new LibrarianController();

			Assert.AreEqual(controller.GetType(), typeof(LibrarianController));
		}

		[TestMethod()]
		public void TestViewAllMemberNoException()
		{
			
			var mockMemberDal = new MockMemberDal
			{
				ThrowError = false
			};
			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());

			var result = (ViewResult)controller.ViewAllMembers();

			Assert.AreEqual("AllMembers", result.ViewName);
			var members = (List<RegisteringMember>) result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestViewAllMemberWithException()
		{
			var mockMemberDal = new MockMemberDal
			{
				ThrowError = true
			};
			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.ViewAllMembers();
			Assert.AreEqual("AllMembers", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var members = (List<RegisteringMember>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestViewMemberHistoryNoException()
		{
			var mockRentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new LibrarianController(new MockMemberDal(), mockRentalDal);
			var result = (ViewResult) controller.ViewMemberHistory("test");
			Assert.AreEqual("MemberHistory", result.ViewName);
			var members = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, members.Count);
		}

		[TestMethod()]
		public void TestViewMemberHistoryWithException()
		{
			var mockRentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var controller = new LibrarianController(new MockMemberDal(), mockRentalDal);
			var result = (ViewResult)controller.ViewMemberHistory("test");
			Assert.AreEqual("MemberHistory", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var members = (List<RentalItem>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestRentalDetailsNoException()
		{
			var mockRentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new LibrarianController(new MockMemberDal(), mockRentalDal);
			var result = (ViewResult)controller.RentalDetails(1);
			Assert.AreEqual("RentalDetails", result.ViewName);
			var members = (List<RentalItem>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestRentalDetailsWithException()
		{
			var mockRentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var controller = new LibrarianController(new MockMemberDal(), mockRentalDal);
			var result = (ViewResult)controller.RentalDetails(1);
			Assert.AreEqual("RentalDetails", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var members = (List<RentalItem>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestMemberFilterAll()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = false
			};
			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult) controller.MemberFilter("All");
			Assert.AreEqual("AllMembers", result.ViewName);
			var members = (List<RegisteringMember>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestMemberFilterOverdue()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = false
			};
			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.MemberFilter("Overdue");
			Assert.AreEqual("AllMembers", result.ViewName);
			var members = (List<RegisteringMember>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestMemberFilterWithException()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = true
			};
			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.MemberFilter("Overdue");
			Assert.AreEqual("AllMembers", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var members = (List<RegisteringMember>)result.Model;
			Assert.AreEqual(0, members.Count);
		}

		[TestMethod()]
		public void TestBlacklistMemberError()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = true
			};

			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.BlacklistMember(15);
			Assert.AreEqual("AllMembers", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var members = (List<RegisteringMember>)result.Model;
			Assert.AreEqual(0, members.Count);
		}


		[TestMethod()]
		public void TestBlacklistMember()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = false
			};

			var controller = new LibrarianController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.BlacklistMember(15);
			Assert.AreEqual("AllMembers", result.ViewName);
		}

	}
}
