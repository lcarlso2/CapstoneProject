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
        /// Gets the inventory items from the db that are in stock or rented 
        /// </summary>
        /// <returns>List of inventory items from the db </returns>
        List<InventoryItem> GetInventoryItems();

        /// <summary>
        /// Gets history of an item and returns a list of rental items for that item
        /// </summary>
        /// <returns>A list of rental items with information about a specified inventory item</returns>
        List<RentalItem> GetItemHistorySummary(int inventoryId);

        /// <summary>
        /// Adds an inventory item to the DB
        /// </summary>
        /// <param name="item">the item being added</param>
        /// @precondition none
        /// @postcondition the item is added
        void AddInventoryItem(InventoryItem item);

        /// <summary>
        /// Removes the inventory item from the db
        /// </summary>
        /// <param name="inventoryId"> the inventory id of the item to be removed </param>
        /// @precondition none
        /// @postcondition the item is removed
        void RemoveInventoryItem(int inventoryId);
    }
}
