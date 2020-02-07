using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentMe.Models;

namespace RentMe.DAL
{
	/// <summary>
	/// The media interface for the interactions with the database concerning media objects
	/// </summary>
	public interface IMediaDal
	{

		/// <summary>
		/// Retrieves all media.
		/// </summary>
		/// <returns>All media </returns>
		List<Media> RetrieveAllMedia();

		/// <summary>
		/// Retrieves all media in a specific category.
		/// </summary>
		/// <returns>All media in a category</returns>
		List<Media> RetrieveMediaByCategory(string category);

		/// <summary>
		/// Retrieves all media in a specific type.
		/// </summary>
		/// <returns>All media in a type</returns>
		List<Media> RetrieveMediaByType(string type);
	}
}
