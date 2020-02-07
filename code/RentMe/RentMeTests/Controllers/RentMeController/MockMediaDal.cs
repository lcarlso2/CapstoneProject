using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.Controllers.RentMeController
{
	/// <summary>
	/// The mock media dal class used for testing purposes 
	/// </summary>
	public class MockMediaDal : IMediaDal
	{

		public List<Media> RetrieveAllMedia()
		{
			var media = new List<Media>{new Media{InventoryId = 1}};
			return media;
		}

		public List<Media> RetrieveMediaByCategory(string category)
		{
			var media = new List<Media>();
			return media;
		}

		public List<Media> RetrieveMediaByType(string type)
		{
			var media = new List<Media>();
			return media;
		}
	}
}
