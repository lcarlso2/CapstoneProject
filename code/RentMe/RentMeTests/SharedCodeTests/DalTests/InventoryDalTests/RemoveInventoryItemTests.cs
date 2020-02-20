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
	/// The test class for removing inventory items 
	/// </summary>
	[TestClass()]
	public class RemoveInventoryItemTests
	{

		[TestMethod()]
		public void RemoveThatDoesNotExistInMedia()
		{
			var item = new InventoryItem
			{
				Type = "Movie",
				Title = "TESTTITLEFORTESTINGREMOVE",
				Category = "Action",
				Condition = "New"

			};
			var inventoryDal = new InventoryDal();

			inventoryDal.AddInventoryItem(item);

			var items = inventoryDal.GetInventoryItems();
			var itemBefore = items.OrderByDescending(itemSort => itemSort.InventoryId).First();

			inventoryDal.RemoveInventoryItem(itemBefore.InventoryId);

			this.cleanDataBaseWhenDoesNotExistInMedia(itemBefore.MediaId, itemBefore.InventoryId);

			var itemsAfterDelete = inventoryDal.GetInventoryItems();

			Assert.AreEqual("TESTTITLEFORTESTINGREMOVE", itemBefore.Title);

			Assert.AreEqual(true, itemBefore.InStock);
			Assert.AreEqual(false, itemBefore.IsRented);

			Assert.AreNotEqual("TESTTITLEFORTESTINGREMOVE", itemsAfterDelete.OrderByDescending(itemSort => itemSort.InventoryId).First().Title);
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
