
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
		public void RegisterTestWithValidCustomer()
		{
			var customer = new RegisteringCustomer
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
			var mockCustomerDal = new MockCustomerDal
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
			var customer = new RegisteringCustomer();
			var mockCustomerDal = new MockCustomerDal
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
			var customer = new RegisteringCustomer();
			var mockCustomerDal = new MockCustomerDal
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
			var result = (ViewResult)controller.Profile();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Profile", result.ViewName);
			var customer = (Customer)result.Model;
			Assert.AreEqual("test", HomeController.CurrentUser.Email);
			HomeController.CurrentUser = null;

		}

		[TestMethod()]
		public void ProfileTestException()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			HomeController.CurrentUser = new Customer { Email = "test" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var user = new Customer { Email = "updated" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
			var result = (ViewResult)controller.UpdateEmail(user);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			var customer = (Customer)result.Model;
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
			HomeController.CurrentUser = new Customer { Email = "test" };
			var user = new Customer { Email = "updated" };
			var controller = new AccountsController(new MockCustomerDal(), rentalDal);
			var result = (ViewResult)controller.UpdateEmail(user);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			HomeController.CurrentUser = null;

		}
	}
}
