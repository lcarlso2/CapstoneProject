using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeDesktop.ViewModel;
using SharedCode.DAL;
using SharedCode.TestObjects;
using System;

namespace RentMeDesktopTests.ViewModel
{
	[TestClass]
	public class BaseViewModelTests
    {
		[TestMethod()]
		public void GetRentalItemsTestIsValid()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var viewModel = new BaseViewModel(rentalDal, new MockEmployeeDal());

			viewModel.GetRentalItems();

			Assert.AreEqual(1, viewModel.RentalItems.Count);
		}

		[TestMethod()]
		[ExpectedException(typeof(Exception))]
		public void GetRentalItemsTestIsInvalid()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var viewModel = new BaseViewModel(rentalDal, new MockEmployeeDal());
			viewModel.GetRentalItems();

		}

		[TestMethod()]
		public void ValidateLoginCredentialsIsValid()
		{
			var employeeDal = new MockEmployeeDal
			{
				AuthenticateValueToReturn = 1,
				ThrowError = false
			};
			var viewModel = new BaseViewModel(new RentalDal(), employeeDal);

			Assert.AreEqual(true, viewModel.ValidateLoginCredentials("user", "pass"));
		}

		[TestMethod()]
		[ExpectedException(typeof(Exception))]
		public void ValidateLoginCredentialsIsInvalid()
		{
			var employeeDal = new MockEmployeeDal
			{
				ThrowError = true
			};
			var viewModel = new BaseViewModel(new RentalDal(),  employeeDal);
			viewModel.ValidateLoginCredentials("user", "pass");

		}
	}
}
