using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.MockDal
{
	/// <summary>
	/// The mock media dal class used for testing purposes 
	/// </summary>
	public class MockMediaDal : IMediaDal
	{

		public bool ThrowError { get; set; }


		public List<Media> RetrieveAllMedia()
		{
			var media = new List<Media>{new Media{InventoryId = 1}};
			if (this.ThrowError)
			{
				throw new Exception();
			}

		
			return media;
		}


		public List<Media> RetrieveMediaByConditions(string categoryCondition, string typeCondition,
			string librarianChoice)
		{
			var media = new List<Media> { new Media { InventoryId = 1 } };
			if (this.ThrowError)
			{
				throw new Exception();
			}


			return media;
		}

		public void RemoveFromLibrariansChoice(int inventoryId)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}


		public void AddToLibrariansChoice(int inventoryId)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}

		public List<Media> RetrieveMediaByCategory(string category)
		{
			var media = new List<Media>();
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return media;
		}

		public List<Media> RetrieveMediaByType(string type)
		{
			var media = new List<Media>();
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return media;
		}
	}
}
