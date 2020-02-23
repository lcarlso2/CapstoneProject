using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.Controllers;
using RentMe.Models;
using RentMeTests.MockDal;
using SharedCode.TestObjects;

namespace RentMeTests.Controllers
{
	/// <summary>
	/// The test class for the borrow controller 
	/// </summary>
	[TestClass()]
	public class BorrowControllerTests
	{

		[TestMethod()]
		public void DefaultControllerTest()
		{
			var controller = new BorrowController();
			Assert.IsInstanceOfType(controller, typeof(BorrowController));
		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithValidInput()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = false
			};
			var mockMediaDal = new MockMediaDal();
			var borrowItem = new Borrow();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal);
			var result = (RedirectToActionResult)controller.ConfirmedBorrow(borrowItem);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithNullReference()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = true
			};
			var mockMediaDal = new MockMediaDal();
			var borrowItem = new Borrow();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal);
			var result = (ViewResult)controller.ConfirmedBorrow(borrowItem);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Please log in again.", result.ViewData["Error"]);
			Assert.AreEqual("Object reference not set to an instance of an object.", result.ViewData["ErrorMessage"]);
		}

		[TestMethod()]
		public void ConfirmedBorrowTestWithException()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = true,
				ThrowNullReference = false
			};
			var mockMediaDal = new MockMediaDal();
			var borrowItem = new Borrow();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal);
			var result = (ViewResult)controller.ConfirmedBorrow(borrowItem);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void CategoryFilterTestWithAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.CategoryFilter("All");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);

		}

		[TestMethod()]
		public void CategoryFilterTestWithoutAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.CategoryFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);

		}


		[TestMethod()]
		public void CategoryFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.CategoryFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void TypeFilterTestWithAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.TypeFilter("All");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);

		}

		[TestMethod()]
		public void TypeFilterTestWithoutAll()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.TypeFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);

		}


		[TestMethod()]
		public void TypeFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.TypeFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);

		}

		[TestMethod()]
		public void BrowseTest()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.Browse();
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);
		}

		[TestMethod()]
		public void BrowseTestWithException()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.Browse();
			Assert.AreEqual(null, result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(0, actualList.Count);
		}

		[TestMethod()]
		public void ConfirmBorrowTest()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (ViewResult)controller.ConfirmBorrow(1);
			Assert.AreEqual(null, result.ViewName);
			var actualItem = (Media)result.Model;
			Assert.AreEqual(1, actualItem.InventoryId);
		}

		[TestMethod()]
		public void ConfirmBorrowTestWithException()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal);
			var result = (RedirectToActionResult)controller.ConfirmBorrow(1);
			Assert.AreEqual("Browse", result.ActionName);
		}
	}
}
