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
			HomeController.CurrentUser = new Customer();
			var result = (RedirectToActionResult)controller.Index();
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
			Assert.AreEqual("Borrow", result.ControllerName);
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
			var controller = new HomeController(customerDal);
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
			var controller = new HomeController(customerDal);
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
			var controller = new HomeController(customerDal);
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
			var controller = new HomeController(customerDal);
			var result = (ViewResult)controller.Login(customer);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Whoops, try again. Something went wrong.", result.ViewData["Error"]);

		}

		

		[TestMethod()]
		public void SignoutTest()
		{
			var controller = new HomeController(new MockCustomerDal());
			var result = (RedirectToActionResult)controller.Signout();
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual(null, HomeController.CurrentUser);

		}


	}


}