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
		/// @postcondition the item is removed  or an error is thrown
		void RemoveFromLibrariansChoice(int inventoryId);


		/// <summary>
		/// Add the item with the given inventory id to the librarians choice list 
		/// </summary>
		/// <param name="inventoryId">the id of the item</param>
		/// @precondition none
		/// @postcondition the item is added  or an error is thrown
		void AddToLibrariansChoice(int inventoryId);

		/// <summary>
		/// Retrieves the given media item with the given id
		/// </summary>
		/// <param name="id"> the inventory id of the item</param>
		/// <returns>the media item with the given id</returns>
		/// @precondition none
		/// @postcondition the desired item is returned or an error is thrown
		Media RetrieveMediaById(int id);

	}
}
