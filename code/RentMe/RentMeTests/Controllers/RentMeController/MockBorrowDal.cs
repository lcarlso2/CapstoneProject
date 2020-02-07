using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.Controllers.RentMeController
{
	/// <summary>
	/// The mock borrow dal class used for testing purposes 
	/// </summary>
	public class MockBorrowDal : IBorrowDal
	{
		/// <summary>
		/// Set to true when a null reference is needed
		/// </summary>
		public bool ThrowNullReference { get; set; }
		
		/// <summary>
		/// Set to true when an exception is needed
		/// </summary>
		public bool ThrowException { get; set; }

		public void BorrowItem(Customer customer, Media media)
		{
			if (ThrowNullReference)
			{
				throw new NullReferenceException();
			}

			if (ThrowException)
			{
				throw new Exception();
			}
		}
	}
}
