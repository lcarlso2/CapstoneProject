using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MediaDalTest
{
	/// <summary>
	/// The test class for adding to librarians choice list
	/// </summary>
	[TestClass()]
	public class AddToLibrariansChoiceTests
	{
		[TestMethod()]
		public void AddToLibrariansChoiceValidTest()
		{
			var mediaDal = new MediaDal();

			mediaDal.AddToLibrariansChoice(1);

			var mediaAfterAdd = mediaDal.RetrieveMediaById(1);

			mediaDal.RemoveFromLibrariansChoice(1);

			var mediaAfterRemove = mediaDal.RetrieveMediaById(1);

			Assert.AreEqual(true, mediaAfterAdd.IsLibrariansChoice);
			Assert.AreEqual(false, mediaAfterRemove.IsLibrariansChoice);
		}
	}
}
