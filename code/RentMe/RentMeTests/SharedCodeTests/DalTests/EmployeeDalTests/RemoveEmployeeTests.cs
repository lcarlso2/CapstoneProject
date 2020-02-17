using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
	/// <summary>
	/// The test class for remove employee
	/// </summary>
	[TestClass()]
	public class RemoveEmployeeTests
	{
		[TestMethod()]
		public void RemoveEmployeeValidTest()
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

			var resultAfterDelete = employeeDal.Authenticate(employee.Username, employee.Password);
			this.cleanDataBase(employee);

			Assert.AreEqual(0, resultAfterDelete);

		}

		private void cleanDataBase(Employee employee)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from employee where username = @username";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;
						cmd.Parameters.AddWithValue("@username", employee.Username);
						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}
						cmd.Parameters.Clear();

						cmd.CommandText =
							"delete from user where userID = last_insert_id()";
						cmd.Parameters.AddWithValue("@userID", employee.EmployeeId);

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}


						transaction.Commit();
					}

				}
				conn.Close();

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}
