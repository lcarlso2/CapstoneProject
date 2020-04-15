using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.RentalDalTests
{
	/// <summary>
	/// The test class for retrieve history of rental transaction
	/// </summary>
	[TestClass()]
	public class RetrieveHistoryForRentalTransactionTests
	{

		[TestMethod()]
		public void RetrieveHistoryForRentalTransactionValidTest()
		{
			var rentalDal = new RentalDal();

			var result = rentalDal.RetrieveHistoryForRentalTransaction(13);

			Assert.AreEqual(0, result.Count);
		}
	}
}
