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
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Gets or sets the category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The constructor for the media
        /// </summary>
        public Media()
        {
        }
    }
}
