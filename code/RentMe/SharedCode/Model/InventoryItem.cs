using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode.Model
{
    /// <summary>
    /// Template for an inventory item
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// The id for an individual inventory item
        /// </summary>
        public int InventoryId;

        /// <summary>
        /// The id for a specific media item
        /// </summary>
        public int MediaId;

        /// <summary>
        /// The condition of the inventory item
        /// </summary>
        public string Condition;

        /// <summary>
        /// The category of the inventory item
        /// </summary>
        public string Category;

        /// <summary>
        /// The title of the inventory item
        /// </summary>
        public string Title;

        /// <summary>
        /// The type of the inventory item
        /// </summary>
        public string Type;

        /// <summary>
        /// True if item is rented and false if it is not
        /// </summary>
        public bool IsRented;

        /// <summary>
        /// True if the item is in stock and false if it is not
        /// </summary>
        public bool InStock;

        /// <summary>
        /// The rental string info
        /// </summary>
        public string InventoryItemInfo => $"Title: {this.Title}\tCategory: {this.Category}\tType: {this.Type}\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        public InventoryItem()
        {

        }
    }
}
