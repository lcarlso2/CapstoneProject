using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeEmployee.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RentMeEmployeeTests.MockDal;
using SharedCode.Model;

namespace RentMeEmployee.Controllers.Tests
{
	/// <summary>
	/// The test class for the home controller of the RentMeEmployee app
	/// </summary>
	[TestClass()]
	public class HomeControllerTests
	{
		[TestMethod()]
		public void UpdateStatusTestValidOutput()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			var result = (ViewResult)controller.UpdateStatus(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var item = (RentalItem) result.Model;
			Assert.AreEqual(1, item.RentalId);
		}

	

		[TestMethod()]
		public void UpdateStatusTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			var result = (ViewResult)controller.UpdateStatus(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
			var item = (RentalItem)result.Model;
			Assert.AreEqual(0, item.RentalId);
		}

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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (RedirectToActionResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("EmployeeLanding", result.ActionName);
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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
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
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (ViewResult)controller.Login(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("Index", result.ViewName);
			Assert.AreEqual("Whoops, try again. Something went wrong.", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void AddEmployeeTestValid()
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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (ViewResult)controller.AddEmployee(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Employee added!", result.ViewData["SuccessMessage"]);

		}

		[TestMethod()]
		public void AddEmployeeTestWithException()
		{
			var employee = new Employee
			{
				Username = "",
				Password = ""
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (ViewResult)controller.AddEmployee(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh...something went wrong", result.ViewData["ErrorMessage"]);
			Assert.AreEqual(0, controller.ModelState.Count);
		}

		[TestMethod()]
		public void EmployeeLandingValidTest()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			var result = (ViewResult)controller.EmployeeLanding();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var items = (List<RentalItem>) result.Model;
			Assert.AreEqual(1, items.Count);

		}

		[TestMethod()]
		public void EmployeeLandingTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			var result = (ViewResult)controller.EmployeeLanding();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
		}

		[TestMethod()]
		public void ManageEmployeesTestValid()
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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (ViewResult)controller.ManageEmployees();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var employees = (List<Employee>)result.Model;
			Assert.AreEqual(0, employees.Count);
		}

		[TestMethod()]
		public void ManageEmployeesTestExceptionThrown()
		{
			var employee = new Employee
			{
				Username = "",
				Password = ""
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (ViewResult)controller.ManageEmployees();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
			var employees = (List<Employee>)result.Model;
			Assert.AreEqual(0, employees.Count);
		}

		[TestMethod()]
		public void ConfirmedUpdateTestValid()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			HomeController.CurrentEmployee = new Employee();
			var result = (RedirectToActionResult)controller.ConfirmedUpdate(new RentalItem { RentalId = 1, Status = "Ordered" });
			
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("EmployeeLanding", result.ActionName);
			HomeController.CurrentEmployee = null;
		}

		[TestMethod()]
		public void ConfirmedUpdateTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			var controller = new HomeController(rentalDal, new MockEmployeeDal());
			var result = (ViewResult)controller.ConfirmedUpdate(new RentalItem());
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("UpdateStatus", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
			
		}

		[TestMethod()]
		public void DeleteEmployeeTestValid()
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
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (RedirectToActionResult)controller.DeleteEmployee("test");
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("ManageEmployees", result.ActionName);

		}

		[TestMethod()]
		public void DeleteEmployeeTestExceptionThrown()
		{
			var employee = new Employee
			{
				Username = "",
				Password = ""
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = true
			};
			var controller = new HomeController(new MockRentalDal(), employeeDal);
			var result = (RedirectToActionResult)controller.DeleteEmployee("test");
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("ManageEmployees", result.ActionName);
		}










	}
}