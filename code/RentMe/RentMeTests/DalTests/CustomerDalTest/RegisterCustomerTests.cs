using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMeTests.DalTests.CustomerDalTest
{
	/// <summary>
	/// The test class for registering a customer
	/// </summary>
	[TestClass()]
	public class RegisterCustomerTests
	{
		[TestMethod()]
		public void RegisterCustomerValidTest()
		{
			var customerDal = new CustomerDal();
			var customer = new RegisteringCustomer
			{
				Address = "Address", 
				ConfirmEmail = "confirmEmail", 
				ConfirmPassword = "confirmPassword", 
				Email = "confirmEmail", 
				First = "TestCustomerForTesting", 
				Last = "TestCustomer",
				Password = "confirmPassword", 
				State = "GA", 
				Zip = "30135"
			};
			customerDal.RegisterCustomer(customer);

			var result = customerDal.Authenticate("confirmEmail", "confirmPassword");
			this.cleanDataBase(customer);
			Assert.AreEqual(1, result);
			
		}

		private void cleanDataBase(RegisteringCustomer customer)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from member where email = @email;";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;

						cmd.Parameters.Add("@email", MySqlDbType.VarChar);
						cmd.Parameters["@email"].Value = customer.Email;
						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}
						cmd.Parameters.Clear();

						cmd.CommandText =
							"delete from user where fname = @fname";
						cmd.Parameters.Add("@fname", MySqlDbType.VarChar);
						cmd.Parameters["@fname"].Value = customer.First;

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
