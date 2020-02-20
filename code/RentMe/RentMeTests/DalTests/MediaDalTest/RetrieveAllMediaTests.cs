using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.SharedCode.MediaDalTest
{
	/// <summary>
	/// The test class for retrieve all media
	/// </summary>
	[TestClass()]
	public class RetrieveAllMediaTests
	{
		[TestMethod()]
		public void RetrieveAllMediaValidTest()
		{
			var mediaDal = new MediaDal();

			var result = mediaDal.RetrieveAllMedia();

			Assert.AreEqual(9, result.Count);
		}
	}
}
