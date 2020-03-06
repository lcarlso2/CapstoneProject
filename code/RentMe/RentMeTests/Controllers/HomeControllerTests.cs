using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using Microsoft.AspNetCore.Mvc;
using RentMe.Models;
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
			HomeController.CurrentUser = new Member();
			var result = (RedirectToActionResult)controller.Index();
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
			Assert.AreEqual("Borrow", result.ControllerName);
		}

		[TestMethod()]
		public void IndexTestWithoutCurrentUser()
		{
			HomeController.CurrentUser = null;
			HomeController.CurrentLibrarian = null;
		
			var controller = new HomeController();
			var result = (ViewResult)controller.Index();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			
		}


		[TestMethod()]
		public void LoginTestWithValidCustomer()
		{
			var customer = new Member
			{
				Email = "email",
				Password = "password"
			};
			var customerDal = new MockMemberDal
			{
				AuthenticateValueToReturn = 1
			};
			var controller = new HomeController(customerDal, new MockLibrarianDal());
			var result = (RedirectToActionResult) controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}


		[TestMethod()]
		public void LoginTestWithValidLibrarian()
		{
			var librarian = new Member
			{
				Email = "email",
				Password = "password"
			};
			var customerDal = new MockMemberDal
			{
				AuthenticateValueToReturn = 0
			};
			var librarianDal = new MockLibrarianDal
			{
				AuthenticateValueToReturn = 1
			};
			var controller = new HomeController(customerDal, librarianDal);
			var result = (RedirectToActionResult)controller.Login(librarian);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}

		[TestMethod()]
		public void LoginTestWithInValidCustomerLogin()
		{
			var customer = new Member
			{
				Email = "email",
				Password = "password"
			};
			var customerDal = new MockMemberDal
			{
				AuthenticateValueToReturn = 2
			};
			var controller = new HomeController(customerDal, new MockLibrarianDal());
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void LoginTestWithInValidCustomer()
		{
			var customer = new Member
			{
				Email = "",
				Password = ""
			};
			var customerDal = new MockMemberDal
			{
				AuthenticateValueToReturn = 1
			};
			var controller = new HomeController(customerDal, new MockLibrarianDal());
			controller.ModelState.AddModelError("Test", "Test");
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void LoginTestWithException()
		{
			var customer = new Member
			{
				Email = "",
				Password = ""
			};
			var customerDal = new MockMemberDal
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(customerDal, new MockLibrarianDal());
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Whoops, try again. Something went wrong.", result.ViewData["Error"]);

		}

		

		[TestMethod()]
		public void SignoutTest()
		{
			var controller = new HomeController(new MockMemberDal(), new MockLibrarianDal());
			var result = (RedirectToActionResult)controller.Signout();
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual(null, HomeController.CurrentUser);

		}


	}


}