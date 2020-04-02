using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MediaDalTest
{
	/// <summary>
	/// The test class for retrieve all media by conditions
	/// </summary>
	[TestClass()]
	public class RetrieveMediaByConditionsTests
	{
		[TestMethod()]
		public void RetrieveMediaWithAllConditions()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveMediaByConditions("All", "All", "All");

			Assert.AreEqual(17, result.Count);
		}

		[TestMethod()]
		public void RetrieveMediaWithDifferentCategory()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveMediaByConditions("Action", "All", "All");

			Assert.AreEqual(7, result.Count);
		}

		[TestMethod()]
		public void RetrieveMediaWithDifferentType()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveMediaByConditions("All", "Movie", "All");

			Assert.AreEqual(11, result.Count);
		}
	}
}
