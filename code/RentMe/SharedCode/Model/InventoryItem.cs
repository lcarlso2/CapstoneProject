using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Model
{
    /// <summary>
    /// Template for an inventory item
    /// </summary>
    public class InventoryItem
    {

        public static List<string> CategoryOptions = new List<string>{ "Action", "Adventure", "Horror", "Comedy", "Romance"};

        public static List<string> TypeOptions = new List<string> { "Movie", "Book", "TV Show"};

        public static List<string> ConditionOptions = new List<string> { "New", "Used", "Poor" };

        /// <summary>
        /// The id for an individual inventory item
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// The id for a specific media item
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// The condition of the inventory item
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// The category of the inventory item
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The title of the inventory item
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The type of the inventory item
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// True if item is rented and false if it is not
        /// </summary>
        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }

        /// <summary>
        /// True if the item is in stock and false if it is not
        /// </summary>
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        public InventoryItem()
        {
	        this.Title = "";
        }
    }
}
