using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeDesktop.ViewModel;
using SharedCode.TestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktopTests.ViewModel
{
	[TestClass]
	public class MainPageViewModelTests
    {
		[TestMethod()]
		public void UpdateRentalItemsTestIsValid()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var viewModel = new MainPageViewModel(rentalDal);

			

			Assert.AreEqual(1, viewModel.UpdateRentalItem());
		}

		[TestMethod()]
		[ExpectedException(typeof(Exception))]
		public void UpdateRentalItemsTestIsInvalid()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = true
			};
			var viewModel = new MainPageViewModel(rentalDal);
			viewModel.UpdateRentalItem();

		}

		[TestMethod()]
		public void SetSelectedStatusFilterWithFilterSetToAll()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var viewModel = new MainPageViewModel(rentalDal);
			viewModel.SelectedStatusFilter = "All";

			Assert.AreEqual(1, viewModel.RentalItems.Count);
		}

		[TestMethod()]
		public void SetSelectedStatusFilterWithFilterNotSetToAll()
		{
			var rentalDal = new MockRentalDal
			{
				ThrowError = false
			};
			var viewModel = new MainPageViewModel(rentalDal);
			viewModel.SelectedStatusFilter = "Ordered";

			Assert.AreEqual(0, viewModel.RentalItems.Count);
		}
	}
}
