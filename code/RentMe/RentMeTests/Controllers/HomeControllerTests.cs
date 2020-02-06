using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RentMe.Models;

namespace RentMeTests.Controllers.Tests
{
	[TestClass()]
	public class HomeControllerTests
	{
		[TestMethod()]
		public void IndexTestWithCurrentUser()
		{

			using (var loggerFactory = new LoggerFactory())
			{
				var logger = loggerFactory.CreateLogger<HomeController>();
				var controller = new HomeController(logger);
				HomeController.CurrentUser = new Customer();
				var result = (RedirectToActionResult)controller.Index();
				Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
				Assert.AreEqual("Browse", result.ActionName);
				
			}
		}

		[TestMethod()]
		public void IndexTestWithoutCurrentUser()
		{
			HomeController.CurrentUser = null;
			using (var loggerFactory = new LoggerFactory())
			{
				var logger = loggerFactory.CreateLogger<HomeController>();
				var controller = new HomeController(logger);
				var result = (ViewResult)controller.Index();
				Assert.IsInstanceOfType(result, typeof(ViewResult));
				Assert.AreEqual("Index", result.ViewName);
			}
		}

		[TestMethod()]
		public void RegisterTest()
		{
			using (var loggerFactory = new LoggerFactory())
			{
				var logger = loggerFactory.CreateLogger<HomeController>();
				var controller = new HomeController(logger);
				var result = (ViewResult)controller.Register();
				Assert.IsInstanceOfType(result, typeof(ViewResult));
				Assert.AreEqual("Register", result.ViewName);
			}
		}

		[TestMethod]
		public void TestTest()
		{
			Assert.AreEqual(1,1);
		}
	}
}