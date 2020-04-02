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
		public void DefaultControllerTestWithNullSelected()
		{
			var controller = new BorrowController();
			BorrowController.SelectedCategory = null;
			BorrowController.SelectedType = null;
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
			var borrowItem = new ConfirmBorrowObject();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal, new MockMemberDal());
			var result = (RedirectToActionResult)controller.ConfirmedBorrow(borrowItem);
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			Assert.AreEqual("Browse", result.ActionName);
		}

		
		[TestMethod()]
		public void ConfirmedBorrowTestWithInvalidInput()
		{
			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = false
			};
			var mockMediaDal = new MockMediaDal();
			var borrowItem = new ConfirmBorrowObject();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal, new MockMemberDal());
			controller.ModelState.AddModelError("Error", "Error");
			var result = (ViewResult)controller.ConfirmedBorrow(borrowItem);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Please select a shipping address", result.ViewData["Error"]);

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
			var borrowItem = new ConfirmBorrowObject();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal, new MockMemberDal());
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
			var borrowItem = new ConfirmBorrowObject();
			var controller = new BorrowController(mockBorrowDal, mockMediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
			var result = (ViewResult)controller.CategoryFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);

		}


		[TestMethod()]
		public void CategoryFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
			var result = (ViewResult)controller.TypeFilter("Action");
			Assert.AreEqual("Browse", result.ViewName);
			var actualList = (List<Media>)result.Model;
			Assert.AreEqual(1, actualList.Count);

		}


		[TestMethod()]
		public void TypeFilterTestWithExceptionThrown()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
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

			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = false,
				NumberToReturn = 1
			};
			HomeController.CurrentUser = new Member();
			var controller = new BorrowController(mockBorrowDal, mediaDal, new MockMemberDal());
			var result = (ViewResult)controller.ConfirmBorrow(1);
			Assert.AreEqual(null, result.ViewName);
			var actualItem = (ConfirmBorrowObject)result.Model;
			Assert.AreEqual(1, BorrowController.SelectedItem.InventoryId);
		}

		[TestMethod()]
		public void ConfirmBorrowTestWithMoreThanAllowedRentals()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = false
			};

			var mockBorrowDal = new MockBorrowDal
			{
				ThrowException = false,
				ThrowNullReference = false,
				NumberToReturn = 3
			};
			HomeController.CurrentUser = new Member();
			var controller = new BorrowController(mockBorrowDal, mediaDal, new MockMemberDal());
			var result = (ViewResult)controller.ConfirmBorrow(1);
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual($"Looks like you have already rented 3 items. Please return something to rent another.", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void ConfirmBorrowTestWithException()
		{
			var mediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mediaDal, new MockMemberDal());
			var result = (RedirectToActionResult)controller.ConfirmBorrow(1);
			Assert.AreEqual("Browse", result.ActionName);
		}

	

		[TestMethod()]
		public void AddAddressTestValid()
		{
			var customerDal = new MockMemberDal()
			{
				ThrowError = false
			};
			HomeController.CurrentUser = new Member();
			var controller = new BorrowController(new MockBorrowDal(), new MockMediaDal(), customerDal);
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
		}

		[TestMethod()]
		public void AddAddressTestInvalid()
		{
			var customerDal = new MockMemberDal()
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), new MockMediaDal(), customerDal);
			controller.ModelState.AddModelError("Error", "Error");
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Invalid Address", result.ViewData["Error"]);
			
		}

		[TestMethod()]
		public void AddAddressTestExceptionThrown()
		{
			var customerDal = new MockMemberDal()
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), new MockMediaDal(), customerDal);
			var result = (ViewResult)controller.AddAddress(new Address());
			Assert.AreEqual("ConfirmBorrow", result.ViewName);
			Assert.AreEqual("Uh-oh something went wrong", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void AddAddressTestPage()
		{
			var controller = new BorrowController(new MockBorrowDal(), new MockMediaDal(), new MockMemberDal());
			var result = (PartialViewResult) controller.AddAddress();
			Assert.AreEqual("AddAddress", result.ViewName);
		}

		[TestMethod()]
		public void AddToLibrariansChoiceValid()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult)controller.AddToLibrariansChoice(1);

			Assert.AreEqual("Browse", result.ViewName);
			

		}

		[TestMethod()]
		public void AddToLibrariansChoiceWithException()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult)controller.AddToLibrariansChoice(1);
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
		}


		[TestMethod()]
		public void RemoveFromLibrariansChoiceValid()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult)controller.RemoveFromLibrariansChoice(1);
			Assert.AreEqual("Browse", result.ViewName);
		}

		[TestMethod()]
		public void RemoveFromLibrariansChoiceWithException()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult)controller.RemoveFromLibrariansChoice(1);
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
		}

		[TestMethod()]
		public void LibrariansChoiceValid()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = false
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult) controller.LibrariansChoice();
			Assert.AreEqual("Browse", result.ViewName);
		}

		[TestMethod()]
		public void LibrariansChoiceWithException()
		{
			var mockMediaDal = new MockMediaDal
			{
				ThrowError = true
			};
			var controller = new BorrowController(new MockBorrowDal(), mockMediaDal, new MockMemberDal());
			var result = (ViewResult)controller.LibrariansChoice();
			Assert.AreEqual("Browse", result.ViewName);
			Assert.AreEqual("Uh-oh.. something went wrong", result.ViewData["Error"]);
		}


	}
}
