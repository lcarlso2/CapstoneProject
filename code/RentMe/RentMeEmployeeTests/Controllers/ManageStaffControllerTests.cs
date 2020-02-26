using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeEmployee.Controllers;
using SharedCode.Model;
using SharedCode.TestObjects;

namespace RentMeEmployeeTests.Controllers
{
	/// <summary>
	/// The tests class for the ManageStaff controller
	/// </summary>
	[TestClass()]
	public class ManageStaffControllerTests
	{

		[TestMethod()]
		public void ControllerDefaultConstructorTest()
		{
			var controller = new ManageStaffController();
			Assert.IsInstanceOfType(controller, typeof(ManageStaffController));
		}


		[TestMethod()]
		public void AddEmployeeTest()
		{
			var controller = new ManageStaffController();
			var result = (ViewResult)controller.AddEmployee();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);

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
			var controller = new ManageStaffController(employeeDal);
			var result = (ViewResult)controller.AddEmployee(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Employee added!", result.ViewData["SuccessMessage"]);

		}

		[TestMethod()]
		public void AddEmployeeTestInValid()
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
			var controller = new ManageStaffController(employeeDal);
			controller.ModelState.AddModelError("test", "test");
			var result = (ViewResult)controller.AddEmployee(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(employee, (Employee)result.Model);

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
			var controller = new ManageStaffController(employeeDal);
			var result = (ViewResult)controller.AddEmployee(employee);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh...something went wrong", result.ViewData["ErrorMessage"]);
			Assert.AreEqual(0, controller.ModelState.Count);
		}

		[TestMethod()]
		public void DeleteEmployeeTestValid()
		{
			var employee = new Employee
			{
				Username = "test",
				Password = "password"
			};
			var employeeDal = new MockEmployeeDal()
			{
				AuthenticateValueToReturn = 1,
				ThrowError = false
			};
			employeeDal.AddEmployee(employee, "password");
			var controller = new ManageStaffController(employeeDal);
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
			var controller = new ManageStaffController(employeeDal);
			var result = (RedirectToActionResult)controller.DeleteEmployee("test");
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("ManageEmployees", result.ActionName);
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
			var controller = new ManageStaffController(employeeDal);
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
			var controller = new ManageStaffController(employeeDal);
			var result = (ViewResult)controller.ManageEmployees();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
			var employees = (List<Employee>)result.Model;
			Assert.AreEqual(0, employees.Count);
		}


		[TestMethod()]
		public void EmployeeHistoryTestValid()
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
			var controller = new ManageStaffController(employeeDal);
			var result = (ViewResult)controller.EmployeeHistory(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("EmployeeHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
		}

		[TestMethod()]
		public void EmployeeHistoryTestExceptionThrown()
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
			var controller = new ManageStaffController(employeeDal);
			var result = (ViewResult)controller.EmployeeHistory(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("EmployeeHistory", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
		}

	}
}
