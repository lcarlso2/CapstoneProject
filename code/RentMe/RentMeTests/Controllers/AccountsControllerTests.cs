
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using RentMe.Models;
using RentMeTests.MockDal;
using SharedCode.Model;
using SharedCode.TestObjects;

namespace RentMeTests.Controllers
{
	/// <summary>
	/// The test class for the accounts controller 
	/// </summary>
	[TestClass()]
	public class AccountsControllerTests
	{

		[TestMethod()]
		public void RegisterTest()
		{

			var controller = new AccountsController();
			var result = (ViewResult)controller.Register();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Register", result.ViewName);

		}

		[TestMethod()]
		public void RegisterTestWithValidMember()
		{
			var customer = new RegisteringMember
			{
				First = "First",
				Last = "Last",
				Email = "Test",
				ConfirmEmail = "Test",
				Password = "Password",
				ConfirmPassword = "Password",
				Address = new Address
				{
					StreetAddress = "Address",
					State = "GA",
					Zip = "31035"
				}
			};
			var mockCustomerDal = new MockMemberDal
			{
				ThrowError = false
			};
			var controller = new AccountsController(mockCustomerDal, new MockRentalDal());
			var result = (ViewResult)controller.Register(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Register", result.ViewName);
			Assert.AreEqual("You're Registered!", result.ViewData["SuccessMessage"]);
		}

		[TestMethod()]
		public void RegisterTestWithInValidCustomer()
		{
			var customer = new RegisteringMember();
			var mockCustomerDal = new MockMemberDal
			{
				ThrowError = false
			};
			var controller = new AccountsController(mockCustomerDal, new MockRentalDal());
			controller.ModelState.AddModelError("test", "test");
			var result = (ViewResult)controller.Register(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(customer, result.Model);

		}

		[TestMethod()]
		public void RegisterTestWithExceptionThrownFromDb()
		{
			var customer = new RegisteringMember();
			var mockCustomerDal = new MockMemberDal
			{
				ThrowError = true
			};
			var controller = new AccountsController(mockCustomerDal, new MockRentalDal());

			var result = (ViewResult)controller.Register(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(customer, result.Model);
			Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.ViewData["ErrorMessage"]);

		}

		[TestMethod()]
		public void RentalFilterTestDateFilter()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalFilter("Date");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("RentalHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalFilterTestStatusFilter()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalFilter("Status");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("RentalHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalFilterTestTitleFilter()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalFilter("Title");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("RentalHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalFilterTestNoFilter()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalFilter("");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("RentalHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalFilterTestException()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (RedirectToActionResult)controller.RentalFilter("");
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalHistoryTest()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalHistory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void RentalHistoryTestException()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.RentalHistory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			HomeController.CurrentUser = null;

		}


		[TestMethod()]
		public void ProfileTest()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.Profile();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);
			var customer = (Member)result.Model;
			Assert.AreEqual("test", HomeController.CurrentUser.Email);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void ProfileTestException()
		{
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = true
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var controller = new AccountsController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.Profile();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void UpdateEmailTest()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var user = new Member { Email = "updated" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.UpdateEmail(user);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			var customer = (Member)result.Model;
			Assert.AreEqual("updated", HomeController.CurrentUser.Email);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void UpdateEmailException()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			HomeController.CurrentUser = new Member { Email = "test" };
			var user = new Member { Email = "updated" };
			var controller = new AccountsController(new MockMemberDal(), rentalDal);
			var result = (ViewResult)controller.UpdateEmail(user);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void AddAddressGetTest()
		{

			var controller = new AccountsController();
			var result = (PartialViewResult) controller.AddAddress();
			Assert.IsInstanceOfType(result, typeof(PartialViewResult));
			Assert.AreEqual("AddAddress", result.ViewName);

		}

		[TestMethod()]
		public void AddAddressValidTest()
		{
			HomeController.CurrentUser = new Member();
			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = false
			};
			var controller = new AccountsController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);

		}

		[TestMethod()]
		public void AddAddressInvalidTest()
		{

			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = false
			};
			var controller = new AccountsController(mockMemberDal, new MockRentalDal());
			controller.ModelState.AddModelError("error", "error");
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);
			Assert.AreEqual("Invalid Address", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void AddAddressExceptionTest()
		{

			var mockMemberDal = new MockMemberDal()
			{
				ThrowError = true
			};
			var controller = new AccountsController(mockMemberDal, new MockRentalDal());
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);


		}
	}
}
