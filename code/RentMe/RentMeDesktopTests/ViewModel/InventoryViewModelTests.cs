using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.TestObjects;
using RentMeDesktop.ViewModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktopTests.ViewModel
{
    [TestClass]
    public class InventoryViewModelTests
    {

        [TestMethod()]
        public void RetrieveAllInventoryItemsTestIsValid()
        {
            var inventoryDal = new MockInventoryDal
            {
                ThrowError = false
            };
            var viewModel = new InventoryViewModel(inventoryDal);

            viewModel.RetrieveAllInventoryItems();

            Assert.AreEqual(1, viewModel.Inventory.Count);
        }

        [TestMethod()]
        public void RetrieveAllInventoryItemsTestIsInValid()
        {
            var inventoryDal = new MockInventoryDal
            {
                ThrowError = false
            };
            var viewModel = new InventoryViewModel(inventoryDal);

            Assert.AreEqual(null, viewModel.Inventory);
        }


        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetSelectedItemDetailSummaryTestIsInValid()
        {
            var inventoryDal = new MockInventoryDal
            {
                ThrowError = false
            };
            var viewModel = new InventoryViewModel(inventoryDal);

           viewModel.GetSelectedItemHistorySummary();
            
        }

    }

}
