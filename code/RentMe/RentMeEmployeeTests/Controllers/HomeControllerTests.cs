using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeEmployee.Controllers;
using Microsoft.AspNetCore.Mvc;
using SharedCode.Model;
using SharedCode.TestObjects;

namespace RentMeEmployeeTests.Controllers
{
	/// <summary>
	/// The test class for the home controller of the RentMeEmployee app
	/// </summary>
	[TestClass()]
	public class HomeControllerTests
	{
		

		[TestMethod()]
		public void LoginTestWithValidEmployee()
		{
			var employee = new Employee
			{
				Username = "Test",
				Password = "Pass"
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = false
			};
			var controller = new HomeController(employeeDal);
			var result = (RedirectToActionResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.IsTrue(controller.ModelState.IsValid);
			Assert.AreEqual("EmployeeLanding", result.ActionName);
			Assert.AreEqual("Orders", result.ControllerName);
		}

		[TestMethod()]
		public void LoginTestWithInValidEmployeeLogin()
		{
			var employee = new Employee
			{
				Username = "Test",
				Password = "Pass"
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 0,
				ThrowError = false
			};
			var controller = new HomeController(employeeDal);
			var result = (ViewResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void LoginTestWithInValidEmployee()
		{
			var employee = new Employee
			{
				Username = "",
				Password = ""
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = false
			};
			var controller = new HomeController(employeeDal);
			controller.ModelState.AddModelError("Test", "Test");
			var result = (ViewResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Invalid login", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void LoginTestWithException()
		{
			var employee = new Employee
			{
				Username = "",
				Password = ""
			};
			var mockEmployeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			
			var controller = new HomeController(mockEmployeeDal);
			Assert.IsTrue(controller.ModelState.IsValid);
			var result = (ViewResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
		}


		[TestMethod()]
		public void IndexTestEmployeeNotNull()
		{
			HomeController.CurrentEmployee = new Employee();
			var controller = new HomeController();
			var result = (RedirectToActionResult) controller.Index();
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("EmployeeLanding", result.ActionName);
			HomeController.CurrentEmployee = null;
		}

		[TestMethod()]
		public void IndexTestEmployeeNull()
		{
			HomeController.CurrentEmployee = null;
			var controller = new HomeController();
			var result = (ViewResult) controller.Index();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);

		}

		[TestMethod()]
		public void SignoutTest()
		{
			HomeController.CurrentEmployee = null;
			var controller = new HomeController();
			var result = (RedirectToActionResult)controller.SignOut();
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual(null, HomeController.CurrentEmployee);

		}

		[TestMethod()]
		public void LoginTest()
		{
			var controller = new HomeController();
			var result = (ViewResult)controller.Login();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			
		}



	}
}