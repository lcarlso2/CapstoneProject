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
	public class AddEmployeeTests
	{
		[TestMethod()]
		public void AddEmployeeValidTest()
		{
			var employeeDal = new EmployeeDal();
			var employee = new Employee
			{
				FirstName = "TestEmployeeForTesting",
				LastName = "TestEmployeeForTesting",
				Username = "TestUsernameForTest",
				IsManager = false,
				Password = "testpasswordfortesting"
			};
			employeeDal.AddEmployee(employee, "testpasswordfortesting");

			var result = employeeDal.Authenticate(employee.Username, employee.Password);

			Assert.AreEqual(1, result);

			employeeDal.RemoveEmployee(employee.Username);

			var resultAfterDDelete = employeeDal.Authenticate(employee.Username, employee.Password);

			Assert.AreEqual(0, resultAfterDDelete);

		}

	}
}
