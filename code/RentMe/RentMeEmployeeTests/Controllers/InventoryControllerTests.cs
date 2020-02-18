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
	/// The tests class for the inventory controller 
	/// </summary>
	[TestClass()]
	public class InventoryControllerTests
	{

		[TestMethod()]
		public void ControllerDefaultConstructorTest()
		{
			var controller = new InventoryController();
			Assert.IsInstanceOfType(controller, typeof(InventoryController));
		}


		[TestMethod()]
		public void InventoryItemDetailsValidTest()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.InventoryItemHistory(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ItemHistory", result.ViewName);
			var items = (List<RentalItem>)result.Model;
			Assert.AreEqual(1, items.Count);
		}

		[TestMethod()]
		public void InventoryItemDetailsTestExceptionThrown()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = true
			};
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.InventoryItemHistory(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ViewInventory", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
		}

		[TestMethod()]
		public void ViewInventoryValidTest()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.ViewInventory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			var items = (List<InventoryItem>)result.Model;
			Assert.AreEqual(1, items.Count);
		}

		[TestMethod()]
		public void ViewInventoryTestExceptionThrown()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = true
			};
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.ViewInventory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["ErrorMessage"]);
		}


	}
}
