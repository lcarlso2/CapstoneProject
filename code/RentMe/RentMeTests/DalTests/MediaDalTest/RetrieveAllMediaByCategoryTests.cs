using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MediaDalTest
{
	/// <summary>
	/// The test class for retrieve all media by category
	/// </summary>
	[TestClass()]
	public class RetrieveAllMediaByCategoryTests
	{
		[TestMethod()]
		public void RetrieveAllMediaByCategoryValidTest()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveMediaByCategory("Action");

			Assert.AreEqual(5, result.Count);
		}
	}
}
