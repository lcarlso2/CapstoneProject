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
		/// Retrieves media with specific conditions
		/// </summary>
		/// <param name="categoryCondition">the category condition</param>
		/// <param name="typeCondition"> the type condition</param>
		/// <param name="librarianChoice">the librarians choice option</param>
		/// <returns>All media with specific conditions</returns>
		List<Media> RetrieveMediaByConditions(string categoryCondition, string typeCondition, string librarianChoice);

		/// <summary>
		/// Removes the item with the given inventory id from the librarians choice list 
		/// </summary>
		/// <param name="inventoryId">the id of the item</param>
		/// @precondition none
		/// @postcondition the item is removed 
		void RemoveFromLibrariansChoice(int inventoryId);


		/// <summary>
		/// Add the item with the given inventory id to the librarians choice list 
		/// </summary>
		/// <param name="inventoryId">the id of the item</param>
		/// @precondition none
		/// @postcondition the item is added 
		void AddToLibrariansChoice(int inventoryId);

		/// <summary>
		/// Retrieves all media in a specific category.
		/// </summary>
		/// <returns>All media in that category</returns>
		List<Media> RetrieveMediaByCategory(string category);

		/// <summary>
		/// Retrieves all media in a specific type.
		/// </summary>
		/// <returns>All media in that type</returns>
		List<Media> RetrieveMediaByType(string type);
	}
}
