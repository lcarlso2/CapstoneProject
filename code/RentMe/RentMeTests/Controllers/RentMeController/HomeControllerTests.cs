using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using RentMe.DAL;
using RentMe.Models;
using RentMeTests.Controllers.RentMeController;

namespace RentMeTests.Controllers.Tests
{
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
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), mockCustomerDal, new MockMediaDal());
			
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
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal);
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
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal);
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
			var controller = new HomeController(mockBorrowDal, new MockCustomerDal(), mockMediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), customerDal, new MockMediaDal());
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
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
			var controller = new HomeController(new MockBorrowDal(), new CustomerDal(), mediaDal);
			var result = (ViewResult)controller.Browse();
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);
		}

	}


}