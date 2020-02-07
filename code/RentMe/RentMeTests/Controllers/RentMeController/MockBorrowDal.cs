using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.Controllers.RentMeController
{
	public class MockBorrowDal : IBorrowDal
	{
		public bool ThrowNullReference { get; set; }
		
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
