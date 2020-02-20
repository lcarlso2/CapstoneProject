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

		[TestMethod()]
		public void AddInventoryTestGetMethod()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.AddInventory();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			
		}

		[TestMethod()]
		public void AddInventoryTestPostMethodValidItem()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};

			var item = new InventoryItem();
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.AddInventory(item);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Item Added!", result.ViewData["SuccessMessage"]);

		}

		[TestMethod()]
		public void AddInventoryTestPostMethodInvalidItem()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};

			var item = new InventoryItem();
			var controller = new InventoryController(mockInventoryDal);
			controller.ModelState.AddModelError("test", "test");
			var result = (ViewResult)controller.AddInventory(item);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(item, (InventoryItem)result.Model);

		}

		[TestMethod()]
		public void AddInventoryTestPostMethodExceptionThrown()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = true
			};

			var item = new InventoryItem();
			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.AddInventory(item);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual(item, (InventoryItem)result.Model);
			Assert.AreEqual("Uh-oh, something bad happened", result.ViewData["ErrorMessage"]);

		}

		[TestMethod()]
		public void RemoveInventoryItemValid()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = false
			};

			var controller = new InventoryController(mockInventoryDal);
			var result = (RedirectToActionResult)controller.RemoveInventoryItem(1);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("ViewInventory", result.ActionName);
			

		}

		[TestMethod()]
		public void RemoveInventoryItemExceptionThrown()
		{
			var mockInventoryDal = new MockInventoryDal()
			{
				ThrowError = true
			};

			var controller = new InventoryController(mockInventoryDal);
			var result = (ViewResult)controller.RemoveInventoryItem(1);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ViewInventory", result.ViewName);
			Assert.AreEqual("Uh-oh something bad happened", result.ViewData["ErrorMessage"]);
			var items = (List<InventoryItem>) result.Model;
			Assert.AreEqual(0, items.Count);
		}

	}
}
