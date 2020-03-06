using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMeTests.DalTests.CustomerDalTest
{

	/// <summary>
	/// The test class for adding an address to the db for a customer 
	/// </summary>
	[TestClass()]
	public class AddAddressTests
	{
		[TestMethod()]
		public void AddAddress()
		{
			var customer = new Member { Email = "email@email.com"};
			var customerDal = new MemberDal();

			customerDal.AddAddress(
					new Address
					{
						StreetAddress = "TEST STREET ADDRESS FOR TESTING", State = "TEST STATE", Zip = "12345"
					},
					customer
				);

			var result = customerDal.GetAddresses(customer);

			this.cleanDataBase();

			Assert.AreEqual("TEST STREET ADDRESS FOR TESTING", result.OrderByDescending(address => address.AddressId).First().StreetAddress);
		}

		private void cleanDataBase()
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from address order by addressID desc limit 1;";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;


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
