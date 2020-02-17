using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
	/// <summary>
	/// The test class for get current user
	/// </summary>
	[TestClass()]
	public class GetCurrentUserTests
	{

		[TestMethod()]
		public void GetCurrentUserValidTest()
		{
			var employeeDal = new EmployeeDal();

			var result = employeeDal.GetCurrentUser("Username", "Password");

			Assert.AreEqual("Username", result.Username);
			Assert.AreEqual("EmployeeFirst", result.FirstName);
			Assert.IsFalse(result.IsManager);
		}
	}
}
