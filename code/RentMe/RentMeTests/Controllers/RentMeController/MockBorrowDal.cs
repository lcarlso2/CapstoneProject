using System;
using System.Collections.Generic;
using System.Text;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.Controllers.RentMeController
{
	public class MockBorrowDal : IBorrowDal
	{

		public void BorrowItem(Customer customer, Media media)
		{

		}
	}
}
