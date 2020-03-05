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

		public int NumberToReturn { get; set; }

		public int BorrowItem(Member member, Media media, int addressId)
		{
			if (this.ThrowNullReference)
			{
				throw new NullReferenceException();
			}

			if (this.ThrowException)
			{
				throw new Exception();
			}

			return 3;
		}

		public int GetNumberOfOpenRentals(Member customer)
		{
			if (this.ThrowException)
			{
				throw new Exception();
			}

			return this.NumberToReturn;
		}
	}
}
