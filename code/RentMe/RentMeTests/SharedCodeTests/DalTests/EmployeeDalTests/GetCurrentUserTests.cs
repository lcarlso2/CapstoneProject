using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
	/// <summary>
	/// The test class for add employee
	/// </summary>
	[TestClass()]
	public class GetCurrentUserTests
	{

		[TestMethod()]
		public void AddEmployeeValidTest()
		{
			var employeeDal = new EmployeeDal();

			var result = employeeDal.GetCurrentUser("lukec", "luke");

			Assert.AreEqual("lukec", result.Username);
			Assert.AreEqual("Luke", result.FirstName);
			Assert.IsTrue(result.IsManager);
		}
	}
}
