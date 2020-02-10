using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RentMe.Models;
using SharedCode.Model;
using SharedCode.TestObjects;
using RentMeTests.MockDal;

namespace RentMeTests.Controllers
{
	/// <summary>
	/// The test class for the home controller of the RentMe project
	/// </summary>
	[TestClass()]
	public class HomeControllerTests
	{
		[TestMethod()]
		public void IndexTestWithCurrentUser()
		{

		
			var controller = new HomeController();
			HomeController.CurrentUser = new Customer();
			var result = (RedirectToActionResult)controller.Index();
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
			
		}

		[TestMethod()]
		public void IndexTestWithoutCurrentUser()
		{
			HomeController.CurrentUser = null;
		
			var controller = new HomeController();
			var result = (ViewResult)controller.Index();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			
		}

		[TestMethod()]
		public void RegisterTest()
		{
			
			var controller = new HomeController();
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
				Address = "Address",
				State = "GA",
				Zip = "31035"
			};
			var mockCustomerDal = new MockCustomerDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal(), new MockRentalDal());
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
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal(), new MockRentalDal());
			controller.ModelState.AddModelError("test","test");
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
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal(), new MockRentalDal());
			
			var result = (ViewResult)controller.Register(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(customer, result.Model);
			Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.ViewData["ErrorMessage"]);

		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithValidInput()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = false
			};
			var mockMediaDal = new MockMediaDal();
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal, new MockRentalDal());
			var result = (RedirectToActionResult)controller.ConfirmedBorrow(1);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithNullReference()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = true
			};
			var mockMediaDal = new MockMediaDal();
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal, new MockRentalDal());
			var result = (ViewResult)controller.ConfirmedBorrow(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Please log in again.", result.ViewData["Error"]);
			Assert.AreEqual("Object reference not set to an instance of an object.", result.ViewData["ErrorMessage"]);
		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithException()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = true,
				ThrowNullReference = false
			};
			var mockMediaDal = new MockMediaDal();
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal, new MockRentalDal());
			var result = (ViewResult)controller.ConfirmedBorrow(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void LoginTestWithValidCustomer()
		{
			var customer = new Customer
			{
				Email = "email",
				Password = "password"
			};
			var customerDal = new MockCustomerDal
			{
				AuthenticateValueToReturn = 1
			};
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal(), new MockRentalDal());
			var result = (RedirectToActionResult) controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}

		[TestMethod()]
		public void LoginTestWithInValidCustomerLogin()
		{
			var customer = new Customer
			{
				Email = "email",
				Password = "password"
			};
			var customerDal = new MockCustomerDal
			{
				AuthenticateValueToReturn = 2
			};
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal(), new MockRentalDal());
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void LoginTestWithInValidCustomer()
		{
			var customer = new Customer
			{
				Email = "",
				Password = ""
			};
			var customerDal = new MockCustomerDal
			{
				AuthenticateValueToReturn = 1
			};
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal(), new MockRentalDal());
			controller.ModelState.AddModelError("Test", "Test");
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void LoginTestWithException()
		{
			var customer = new Customer
			{
				Email = "",
				Password = ""
			};
			var customerDal = new MockCustomerDal
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal(), new MockRentalDal());
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Whoops, try again. Something went wrong.", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void CategoryFilterTestWithAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.CategoryFilter("All");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>) result.Model;
			Assert.AreEqual(1, actualList.Count);

		}

		[TestMethod()]
		public void CategoryFilterTestWithoutAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.CategoryFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);

		}


		[TestMethod()]
		public void CategoryFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.CategoryFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void TypeFilterTestWithAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.TypeFilter("All");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);

		}

		[TestMethod()]
		public void TypeFilterTestWithoutAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.TypeFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);

		}


		[TestMethod()]
		public void TypeFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.TypeFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void BrowseTest()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.Browse();
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);
		}

		[TestMethod()]
		public void BrowseTestWithException()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.Browse();
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);
		}

		[TestMethod()]
		public void ConfirmBorrowTest()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (ViewResult)controller.ConfirmBorrow(1);
			Assert.AreEqual(null, result.ViewName);
			var actualItem = (Media)result.Model;
			Assert.AreEqual(1, actualItem.InventoryId);
		}

		[TestMethod()]
		public void ConfirmBorrowTestWithException()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), mediaDal, new MockRentalDal());
			var result = (RedirectToActionResult)controller.ConfirmBorrow(1);
			Assert.AreEqual("Browse", result.ActionName);
		}

		[TestMethod()]
		public void SignoutTest()
		{
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), new MockRentalDal());
			var result = (RedirectToActionResult)controller.Signout();
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual(null, HomeController.CurrentUser);

		}


		[TestMethod()]
		public void RentalFilterTestDateFilter()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			}; 
			HomeController.CurrentUser = new Customer{Email = "test"};
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
			var result = (ViewResult) controller.RentalFilter("Date");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("RentalHistory", result.ViewName);
			var items = (List<RentalItem>) result.Model;
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
			var result = (ViewResult)controller.RentalFilter("");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
			var result = (ViewResult)controller.RentalHistory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var items = (List<RentalItem>) result.Model;
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
			var controller = new HomeController(new MockBorrowDal(), new MockCustomerDal(), new MockMediaDal(), rentalDal);
			var result = (ViewResult)controller.RentalHistory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			HomeController.CurrentUser = null;

		}

	}


}