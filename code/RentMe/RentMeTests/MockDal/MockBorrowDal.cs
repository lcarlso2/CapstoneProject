using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.MockDal
{
	/// <summary>
	/// The mock borrow dal class used for testing purposes 
	/// </summary>
	public class MockBorrowDal : IBorrowDal
	{

		public bool ThrowNullReference { get; set; }
		
	
		public bool ThrowException { get; set; }

		public void BorrowItem(Customer customer, Media media)
		{
			if (this.ThrowNullReference)
			{
				throw new NullReferenceException();
			}

			if (this.ThrowException)
			{
				throw new Exception();
			}
		}
	}
}
