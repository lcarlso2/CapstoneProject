using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeTests.SharedCodeTests.DalTests.InventoryDalTests
{
	/// <summary>
	/// The test class for adding inventory items 
	/// </summary>
	[TestClass()]
	public class AddInventoryItemTests
	{
		[TestMethod()]
		public void AddItemThatExistsInMedia()
		{
			var item = new InventoryItem
			{
				Type = "Movie",
				Title = "Superman",
				Category = "Action",
				Condition = "New"

			};
			var inventoryDal = new InventoryDal();

			inventoryDal.AddInventoryItem(item);

			var items = inventoryDal.GetInventoryItems();
			this.cleanDataBaseWhenAlreadyExistsInMedia(items.OrderByDescending(itemSort => itemSort.InventoryId).First().InventoryId);
			var itemsAfterDelete = inventoryDal.GetInventoryItems();
			Assert.AreEqual("Superman", items.OrderByDescending(itemSort => itemSort.InventoryId).First().Title);
			Assert.AreNotEqual("Superman", itemsAfterDelete.OrderByDescending(itemSort => itemSort.InventoryId).First().Title);

		}

		[TestMethod()]
		public void AddItemThatDoesNotExistInMedia()
		{
			var item = new InventoryItem
			{
				Type = "Movie",
				Title = "TESTTITLEFORTESTING",
				Category = "Action",
				Condition = "New"

			};
			var inventoryDal = new InventoryDal();

			inventoryDal.AddInventoryItem(item);

			var items = inventoryDal.GetInventoryItems();
			var itemAdded = items.OrderByDescending(itemSort => itemSort.InventoryId).First();
			this.cleanDataBaseWhenDoesNotExistInMedia(itemAdded.MediaId, itemAdded.InventoryId);
			var itemsAfterDelete = inventoryDal.GetInventoryItems();
			Assert.AreEqual("TESTTITLEFORTESTING", items.OrderByDescending(itemSort => itemSort.InventoryId).First().Title);
			Assert.AreNotEqual("TESTTITLEFORTESTING", itemsAfterDelete.OrderByDescending(itemSort => itemSort.InventoryId).First().Title);
		}

		private void cleanDataBaseWhenAlreadyExistsInMedia(int inventoryId)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from inventory_item where inventoryID = last_insert_id()";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}

						cmd.Parameters.Clear();
						cmd.CommandText = "alter table inventory_item AUTO_INCREMENT = @id";
						cmd.Parameters.AddWithValue("@id", inventoryId);

						cmd.ExecuteNonQuery();

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


		private void cleanDataBaseWhenDoesNotExistInMedia(int mediaId, int inventoryId)
		{
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "delete from inventory_item where inventoryID = last_insert_id()";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}
						cmd.Parameters.Clear();

						cmd.CommandText =
							"delete from media where mediaID = @mediaID";
						cmd.Parameters.AddWithValue("@mediaID", mediaId);

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}

						cmd.Parameters.Clear();
						cmd.CommandText = "alter table inventory_item AUTO_INCREMENT = @inventoryId; " +
										  "alter table media AUTO_INCREMENT = @mediaId;";
						cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
						cmd.Parameters.AddWithValue("@mediaId", mediaId);

						cmd.ExecuteNonQuery();

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
