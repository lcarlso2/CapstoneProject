using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMeTests.SharedCodeTests.DalTests.RentalDalTests
{
	/// <summary>
	/// The test class update status
	/// </summary>
	[TestClass()]
	public class UpdateStatusTests
	{
		[TestMethod()]
		public void UpdateStatusToShippedTest()
		{
			var borrowDal = new BorrowDal();
			var customer = new Member
			{
				Email = "email@email.com",
				Password = "password"
			};

			var media = new Media
			{
				InventoryId = 1
			};

			borrowDal.BorrowItem(customer, media, 1);
			var rentalId = this.getLastRentalTransactionID();

			var rentalDal = new RentalDal();

			rentalDal.UpdateStatus(rentalId, "Shipped", 2, "New");

			var result = rentalDal.RetrieveSelectRentedItems("Shipped");
			var selectedItem = result.First(item => item.RentalId == rentalId);
			this.deleteStatusUpdates(rentalId);
			this.deleteRentalTransaction(rentalId, media);
			Assert.AreEqual("Shipped", selectedItem.Status);

			
		}

		[TestMethod()]
		public void UpdateStatusToReturnedTest()
		{
			var borrowDal = new BorrowDal();
			var customer = new Member
			{
				Email = "email@email.com",
				Password = "password"
			};

			var media = new Media
			{
				InventoryId = 1
			};

			borrowDal.BorrowItem(customer, media, 1);
			var rentalId = this.getLastRentalTransactionID();

			var rentalDal = new RentalDal();

			rentalDal.UpdateStatus(rentalId, "Returned", 2, "New");

			var result = rentalDal.RetrieveSelectRentedItems("Returned");
			var selectedItem = result.First(item => item.RentalId == rentalId);
			this.deleteReturnCondition(rentalId);
			this.deleteStatusUpdates(rentalId);
			this.deleteRentalTransaction(rentalId, media);
			Assert.AreEqual("Returned", selectedItem.Status);

			
		}

		private void deleteReturnCondition(int rentalId)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from return_condition where rentalID = @rentalID";

					using var cmd = new MySqlCommand(query, conn);
					cmd.Transaction = transaction;
					cmd.Parameters.AddWithValue("@rentalID", rentalId);
					if (cmd.ExecuteNonQuery() != 1)
					{
						transaction.Rollback();
					}
					transaction.Commit();

				}
				conn.Close();

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		private void deleteStatusUpdates(int rentalId)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from status_history where rentalTransactionID = @rentalID;";

					using var cmd = new MySqlCommand(query, conn);
					cmd.Transaction = transaction;
					cmd.Parameters.AddWithValue("@rentalID", rentalId);
					if (cmd.ExecuteNonQuery() != 2)
					{
						transaction.Rollback();
					}
					transaction.Commit();

				}
				conn.Close();

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}



		private void deleteRentalTransaction(int rentalId, Media media)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from rental_transaction where rentalID = @rentalID";

					using var cmd = new MySqlCommand(query, conn);
					cmd.Transaction = transaction;
					cmd.Parameters.AddWithValue("@rentalID", rentalId);
					if (cmd.ExecuteNonQuery() != 1)
					{
						transaction.Rollback();
					}
					cmd.Parameters.Clear();

					cmd.CommandText =
						"update inventory_item set isRented = false, inStock = true where inventoryID = @inventoryID;";
					cmd.Parameters.Add("@inventoryID", MySqlDbType.Int32);
					cmd.Parameters["@inventoryID"].Value = media.InventoryId;


					if (cmd.ExecuteNonQuery() != 1)
					{
						transaction.Rollback();
					}
					transaction.Commit();

				}
				conn.Close();

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		private int getLastRentalTransactionID()
		{
			var id = 0;
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					var query = "select max(rental_transaction.rentalID) as rentalID from rental_transaction ORDER BY rental_transaction.rentalID;";
					using (var cmd = new MySqlCommand(query, conn))
					{
						using (var reader = cmd.ExecuteReader())
						{
							var rentalIDOrdinal = reader.GetOrdinal("rentalID");
							while (reader.Read())
							{
								id = reader.GetInt32(rentalIDOrdinal);
							}
						}
					}
					conn.Close();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return id;
		}
	}
}
