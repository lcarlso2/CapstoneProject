using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.Model;

namespace RentMeTests.SharedCodeTests.ModelTests
{
	/// <summary>
	/// The test class for the employee
	/// </summary>
	[TestClass()]
	public class EmployeeTests
	{

		[TestMethod()]
		public void TestCreateEmployeeBaseConstructor()
		{
			var employee = new Employee();

			Assert.AreEqual("", employee.FirstName);
			Assert.AreEqual("", employee.LastName);
		}

		[TestMethod()]
		public void TestCreateEmployee2ParamConstructor()
		{
			var employee = new Employee("Bob", "Jack");

			Assert.AreEqual("Bob", employee.FirstName);
			Assert.AreEqual("Jack", employee.LastName);
			Assert.AreEqual(false, employee.IsManager);
		}

		[TestMethod()]
		public void TestCreateEmployee4ParamConstructor()
		{
			var employee = new Employee("Bob", "Jack", "bjack", true);

			Assert.AreEqual("Bob", employee.FirstName);
			Assert.AreEqual("Jack", employee.LastName);
			Assert.AreEqual("bjack", employee.Username);
			Assert.AreEqual(true, employee.IsManager);
		}

		[TestMethod()]
		public void TestEmployeeInfo()
		{
			var employee = new Employee("Bob", "Jack", "bjack", true);

			Assert.AreEqual($"Name: {employee.FirstName} {employee.LastName} \nUsername: {employee.Username} \nIs Manager: {employee.IsManager} \n", employee.EmployeeInfo);

		}

		[TestMethod()]
		public void TestEmployeePassword()
		{
			var employee = new Employee("Bob", "Jack", "bjack", true);
			employee.Password = "test";

			Assert.AreEqual("test", employee.Password);

		}

		[TestMethod()]
		public void TestEmployeeConfirmPassword()
		{
			var employee = new Employee("Bob", "Jack", "bjack", true);
			employee.ConfirmPassword = "test";

			Assert.AreEqual("test", employee.ConfirmPassword);

		}

		[TestMethod()]
		public void TestEmployeeId()
		{
			var employee = new Employee("Bob", "Jack", "bjack", true);
			employee.EmployeeId = 1;

			Assert.AreEqual(1, employee.EmployeeId);

		}
	}
}
