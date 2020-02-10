
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMeDesktop.ViewModel;
using SharedCode.TestObjects;

namespace RentMeDesktopTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetRentalItemsTest()
        {
	        var rentalDal = new MockRentalDal
	        {
		        ThrowError = false
	        };
            var viewmodel = new BaseViewModel(rentalDal, new MockEmployeeDal());
            viewmodel.GetRentalItems();
            Assert.AreEqual(1,viewmodel.RentalItems.Count);
        }
    }
}
