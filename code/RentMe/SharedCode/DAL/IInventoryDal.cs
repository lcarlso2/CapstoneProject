using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.Model;

namespace SharedCode.DAL
{
    /// <summary>
    /// The interface for all inventory database actions
    /// </summary>
    interface IInventoryDal
    {

        /// <summary>
        /// Gets the inventory from the db
        /// </summary>
        /// <returns>List of inventory items from the db </returns>
        List<InventoryItem> GetInventoryItems();

    }
}
