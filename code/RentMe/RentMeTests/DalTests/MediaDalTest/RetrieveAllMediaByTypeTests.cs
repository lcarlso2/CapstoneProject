using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MediaDalTest
{
	/// <summary>
	/// The test class for retrieve all media by type
	/// </summary>
	[TestClass()]
	public class RetrieveAllMediaByTypeTests
	{
		[TestMethod()]
		public void RetrieveAllMediaByTypeValidTest()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveMediaByType("Movie");

			Assert.AreEqual(11, result.Count);
		}
	}
}
