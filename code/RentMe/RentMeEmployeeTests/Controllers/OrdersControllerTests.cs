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
	/// The tests class for the orders controller 
	/// </summary>
	[TestClass()]
	public class OrdersControllerTests
	{

		[TestMethod()]
		public void ControllerDefaultConstructorTest()
		{
	
			var controller = new OrdersController();
			Assert.IsInstanceOfType(controller, typeof(OrdersController));
		}

		[TestMethod()]
		public void UpdateStatusTestValidOutput()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.UpdateStatus(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var item = (RentalItem)result.Model;
			Assert.AreEqual(1, item.RentalId);
		}



		[TestMethod()]
		public void UpdateStatusTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.UpdateStatus(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
			var item = (RentalItem)result.Model;
			Assert.AreEqual(0, item.RentalId);
		}

		[TestMethod()]
		public void ConfirmedUpdateTestValid()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			HomeController.CurrentEmployee = new Employee();
			var result = (RedirectToActionResult)controller.ConfirmedUpdate(new RentalItem { RentalId = 1, Status = "Ordered" });

			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("EmployeeLanding", result.ActionName);
			HomeController.CurrentEmployee = null;
		}

		[TestMethod()]
		public void ConfirmedUpdateTestInvalid()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			controller.ModelState.AddModelError("test", "test");
			HomeController.CurrentEmployee = new Employee();
			var result = (ViewResult)controller.ConfirmedUpdate(new RentalItem { RentalId = 1, Status = "Ordered" });

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("UpdateStatus", result.ViewName);
			HomeController.CurrentEmployee = null;
		}

		[TestMethod()]
		public void ConfirmedUpdateTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.ConfirmedUpdate(new RentalItem());
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("UpdateStatus", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);

		}

		[TestMethod()]
		public void EmployeeLandingValidTest()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.EmployeeLanding();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);

		}

		[TestMethod()]
		public void EmployeeLandingTestExceptionThrown()
		{
			var rentalDal = new MockRentalDal()
			{
				ThrowError = true
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.EmployeeLanding();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
		}

		[TestMethod()]
		public void StatusFilterTestAll()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.StatusFilter("All");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("EmployeeLanding", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
		}

		[TestMethod()]
		public void StatusFilterTestEmpty()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.StatusFilter("");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("EmployeeLanding", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(0, items.Count);
		}

		[TestMethod()]
		public void StatusFilterExceptionThrown()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var controller = new OrdersController(rentalDal);
			var result = (ViewResult)controller.StatusFilter("");
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("EmployeeLanding", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);
			var item = (List<RentalItem>)result.Model;
			Assert.AreEqual(0, item.Count);
		}
	}
}
