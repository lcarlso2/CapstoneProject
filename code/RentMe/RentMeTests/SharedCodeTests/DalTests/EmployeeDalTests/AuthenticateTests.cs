using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
	/// <summary>
	/// The test class for authenticate in the employee dal
	/// </summary>
	[TestClass()]
	public class AuthenticateTests
	{

		[TestMethod()]
		public void AuthenticateEmployeeValidTest()
		{
			var employeeDal = new EmployeeDal();
			var result = employeeDal.Authenticate("lukec", "luke");
			Assert.AreEqual(1, result);

		}
	}
}
