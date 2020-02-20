using SharedCode.DAL;
using SharedCode.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode.TestObjects
{
    public class MockInventoryDal : IInventoryDal
    {

        public bool ThrowError { get; set; }

        public List<InventoryItem> GetInventoryItems()
        {
            var items = new List<InventoryItem> { new InventoryItem { Title = "Lord of the Rings" } };
            if (this.ThrowError)
            {
                throw new Exception();
            }
            return items;
        }

        public List<RentalItem> GetItemHistorySummary(int inventoryId)
        {
            var items = new List<RentalItem> { new RentalItem { Title = "Lord of the Rings" } };
            if (this.ThrowError)
            {
                throw new Exception();
            }
            return items;
        }

        public void AddInventoryItem(InventoryItem item)
        {

        }
    }
}
