using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
	/// <summary>
	/// The test class for add employee
	/// </summary>
	[TestClass()]
	public class GetEmployeesTests
	{

		[TestMethod()]
		public void GetEmployeesValidTest()
		{
			var employeeDal = new EmployeeDal();
			var currentEmployee = new Employee {Username = "testUserNameForTestingPurposes"};
			var result = employeeDal.GetEmployees(currentEmployee);
			Assert.AreEqual(3, result.Count);
		}
	}
}
