using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of the media
	/// </summary>
	/// 
	[Serializable]
	public class Media
	{

        /// <summary>
        /// Gets or sets the inventory id
        /// </summary>
        /// @precondition none
        /// @postcondition the inventory id is set
        public int InventoryId { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        /// @precondition none
        /// @postcondition the title is set
        public string Title { get; set; }


        /// <summary>
        /// Gets or sets the category
        /// </summary>
        /// @precondition none
        /// @postcondition the category is set
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        /// @precondition none
        /// @postcondition the type is set
        public string Type { get; set; }

        /// <summary>
        /// Gets or set the whether or not this item is in the librarians choice
        /// </summary>
        /// @precondition none
        /// @postcondition the value is set
        public bool IsLibrariansChoice { get; set; }

        /// <summary>
        /// The constructor for the media
        /// </summary>
        /// @precondition none
        /// @postcondition a new media object is created
        public Media()
        {
        }
    }
}
