using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;
using RentMe.Models;

namespace RentMeTests.DalTests.CustomerDalTest
{
	/// <summary>
	/// The test class for getting the addresses of the customer
	/// </summary>
	[TestClass()]
	public class GetAddressesTests
	{

		[TestMethod()]
		public void GetAddressesTest()
		{
			var customerDal = new CustomerDal();

			var result = customerDal.GetAddresses(new Customer {Email = "email@email.com"});

			Assert.AreEqual(3, result.Count);
		}
	}
}
