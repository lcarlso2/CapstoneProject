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

		
	}

	
}