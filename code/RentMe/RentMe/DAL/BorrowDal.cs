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
	/// The class responsible for interacting with the database for borrowing items 
	/// </summary>
	public class BorrowDal : IBorrowDal
	{

		/// <summary>
		/// Borrows an item from the database. Throws an error if there is an error on the database
		/// </summary>
		/// <param name="member">the member borrowing the item</param>
		/// <param name="media">the item being borrowed</param>
		/// <param name="addressId"> the id of the address the item is being shipped to </param>
		/// <returns>the number of rows altered in the database</returns>
		/// @precondition none
		/// @postcondition the item is borrowed or an error is thrown if something goes wrong on the database
		public int BorrowItem(Member member, Media media, int addressId)
		{

			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using var transaction = conn.BeginTransaction();
					var query = "insert into rental_transaction(memberID, rentalDateTime, returnDateTime, inventoryID, addressID) " +
								"values ((select memberID from member where email = @memberEmail), @rentalDateTime, @returnDateTime, @inventoryID, @addressId)";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Transaction = transaction;

						cmd.Parameters.Add("@memberEmail", MySqlDbType.VarChar);
						cmd.Parameters["@memberEmail"].Value = member.Email;

						cmd.Parameters.Add("@rentalDateTime", MySqlDbType.DateTime);
						cmd.Parameters["@rentalDateTime"].Value = DateTime.Now;

						cmd.Parameters.Add("@returnDateTime", MySqlDbType.DateTime);
						cmd.Parameters["@returnDateTime"].Value = DateTime.Now.AddDays(14);

						cmd.Parameters.Add("@inventoryID", MySqlDbType.Int32);
						cmd.Parameters["@inventoryID"].Value = media.InventoryId;

						cmd.Parameters.Add("@addressId", MySqlDbType.Int32);
						cmd.Parameters["@addressId"].Value = addressId;

						if (cmd.ExecuteNonQuery() != 1)
						{
							transaction.Rollback();
						}

						cmd.Parameters.Clear();


						cmd.CommandText = "insert into status_history (rentalTransactionID, statusID, updateDateTime, `condition`) values (last_insert_id(), @statusID, @updateDateTime, @condition);";

						cmd.Parameters.Add("@statusID", MySqlDbType.Int32);
						cmd.Parameters["@statusID"].Value = 1;

						cmd.Parameters.Add("@updateDateTime", MySqlDbType.DateTime);
						cmd.Parameters["@updateDateTime"].Value = DateTime.Now;

						cmd.Parameters.Add("@condition", MySqlDbType.VarChar);
						cmd.Parameters["@condition"].Value = media.Condition;


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
			catch (Exception ex)
			{
				throw ex;
			}

			return 3;
		}

		/// <summary>
		/// Gets the number of open rentals a member has
		/// </summary>
		/// <param name="member">the member being checked </param>
		/// <returns>the number of open rentals or an error if something goes wrong </returns>
		public int GetNumberOfOpenRentals(Member member)
		{
			var count = 0;
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					var query = "select count(*) from rental_transaction " +
								"r, user, member, media, inventory_item i, status where " +
								"userID = member.memberID and member.memberID = r.memberID and " +
								"r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and " +
								"status.status != 'Returned' and " +
								"status.statusID = (select max(s1.statusID) from status_history s1 " +
								"where r.rentalID = s1.rentalTransactionID and r.memberId = (select memberID from member where email = @email) " +
								"group by s1.rentalTransactionID);";

					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@email", member.Email);
						count = Convert.ToInt32(cmd.ExecuteScalar());
					}

					conn.Close();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return count;
		}
	}
}
