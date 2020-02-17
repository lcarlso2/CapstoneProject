using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.Model;

namespace SharedCode.DAL
{
    /// <summary>
    /// The interface for all inventory database actions
    /// </summary>
    public interface IInventoryDal
    {

        /// <summary>
        /// Gets the inventory from the db
        /// </summary>
        /// <returns>List of inventory items from the db </returns>
        List<InventoryItem> GetInventoryItems();

        /// <summary>
        /// Gets history of an item and returns a list of rental items for that item
        /// </summary>
        /// <returns>A list of rental items with information about a specified inventory item</returns>
        List<RentalItem> GetItemHistorySummary(int inventoryId);
    }
}
