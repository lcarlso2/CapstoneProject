using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMeTests.DalTests.BorrowDalTest
{
	/// <summary>
	/// The test class for borrow item 
	/// </summary>
	[TestClass()]
	public class BorrowItemTests
	{
		[TestMethod()]
		public void BorrowItemValidTest()
		{
			var borrowDal = new BorrowDal();
			var customer = new Customer
			{
				Email = "email@email.com",
				Password = "password"
			};

			var media = new Media
			{
				InventoryId = 1
			};

			var rowCount = borrowDal.BorrowItem(customer, media);

			this.cleanDataBase(media);
			Assert.AreEqual(3, rowCount);
		}

		private void cleanDataBase(Media media)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from status_history order by rentalTransactionID desc limit 1;"; 

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;
						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}
						cmd.Parameters.Clear();

						cmd.CommandText =
							"delete from rental_transaction order by rentalID desc limit 1";

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}
						cmd.Parameters.Clear();

						cmd.CommandText =
							"update inventory_item set isRented = false where inventoryID = @inventoryID;";
						cmd.Parameters.Add("@inventoryID", MySqlDbType.Int32);
						cmd.Parameters["@inventoryID"].Value = media.InventoryId;


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
