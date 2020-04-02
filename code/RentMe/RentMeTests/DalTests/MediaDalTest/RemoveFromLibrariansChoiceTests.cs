using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.DalTests.MediaDalTest
{
	/// <summary>
	/// The test class for removing from librarians choice list
	/// </summary>
	[TestClass()]
	public class RemoveFromLibrariansChoiceTests
	{
		[TestMethod()]
		public void RemoveFromLibrariansChoiceValidTest()
		{
			var mediaDal = new MediaDal();

			mediaDal.AddToLibrariansChoice(1);
			mediaDal.RemoveFromLibrariansChoice(1);

			var media = mediaDal.RetrieveMediaById(1);

			Assert.AreEqual(false, media.IsLibrariansChoice);
		}

	}
}
