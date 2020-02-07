using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RentMe.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMe.DAL
{
	/// <summary>
	/// The class responsible for borrowing items 
	/// </summary>
	public class BorrowDal : IBorrowDal
	{
		/// <summary>
		/// Borrows an item 
		/// </summary>
		/// <param name="customer"> the customer borrowing an item</param>
		/// <param name="media"> the media being rented</param>
		[Obsolete("Use none static borrow item method instead")]
		public static void OldBorrowItem(Customer customer, Media media)
		{
		
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using (var transaction = conn.BeginTransaction())
					{


						var query = "insert into rental_transaction(memberID, rentalDateTime, returnDateTime, inventoryID) " +
						            "values ((select memberID from member where email = @memberEmail), @rentalDateTime, @returnDateTime, @inventoryID)";

						using (var cmd = new MySqlCommand(query, conn))
						{
							cmd.Transaction = transaction;

							cmd.Parameters.Add("@memberEmail", MySqlDbType.VarChar);
							cmd.Parameters["@memberEmail"].Value = customer.Email;

							cmd.Parameters.Add("@rentalDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@rentalDateTime"].Value = DateTime.Now;

							cmd.Parameters.Add("@returnDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@returnDateTime"].Value = DateTime.Now.AddDays(14);

							cmd.Parameters.Add("@inventoryID", MySqlDbType.Int32);
							cmd.Parameters["@inventoryID"].Value = media.InventoryId;

							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							cmd.Parameters.Clear();
							

							cmd.CommandText = "insert into status_history (rentalTransactionID, statusID, updateDateTime) values (last_insert_id(), @statusID, @updateDateTime);";

							cmd.Parameters.Add("@statusID", MySqlDbType.Int32);
							cmd.Parameters["@statusID"].Value = 1;

							cmd.Parameters.Add("@updateDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@updateDateTime"].Value = DateTime.Now;


							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							cmd.Parameters.Clear();

							cmd.CommandText =
								"update inventory_item set isRented = true where inventoryID = @inventoryID;";
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
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
		public void BorrowItem(Customer customer, Media media)
		{

			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using (var transaction = conn.BeginTransaction())
					{


						var query = "insert into rental_transaction(memberID, rentalDateTime, returnDateTime, inventoryID) " +
									"values ((select memberID from member where email = @memberEmail), @rentalDateTime, @returnDateTime, @inventoryID)";

						using (var cmd = new MySqlCommand(query, conn))
						{
							cmd.Transaction = transaction;

							cmd.Parameters.Add("@memberEmail", MySqlDbType.VarChar);
							cmd.Parameters["@memberEmail"].Value = customer.Email;

							cmd.Parameters.Add("@rentalDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@rentalDateTime"].Value = DateTime.Now;

							cmd.Parameters.Add("@returnDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@returnDateTime"].Value = DateTime.Now.AddDays(14);

							cmd.Parameters.Add("@inventoryID", MySqlDbType.Int32);
							cmd.Parameters["@inventoryID"].Value = media.InventoryId;

							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							cmd.Parameters.Clear();


							cmd.CommandText = "insert into status_history (rentalTransactionID, statusID, updateDateTime) values (last_insert_id(), @statusID, @updateDateTime);";

							cmd.Parameters.Add("@statusID", MySqlDbType.Int32);
							cmd.Parameters["@statusID"].Value = 1;

							cmd.Parameters.Add("@updateDateTime", MySqlDbType.DateTime);
							cmd.Parameters["@updateDateTime"].Value = DateTime.Now;


							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							cmd.Parameters.Clear();

							cmd.CommandText =
								"update inventory_item set isRented = true where inventoryID = @inventoryID;";
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
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
