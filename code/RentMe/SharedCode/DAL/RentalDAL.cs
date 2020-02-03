using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using SharedCode.Model;

namespace SharedCode.DAL
{
	/// <summary>
	/// The rental dal responsible for communicating with the database for the rentals 
	/// </summary>
	public class RentalDal
	{

        /// <summary>
        /// Retrieves all borrowed items.
        /// </summary>
        /// <returns>All borrowed items </returns>
        public static List<RentalItem> RetrieveAllRentedItems()
        {
            var rentedItems = new List<RentalItem>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select member.memberID, email, r.rentalID, rentalDateTime, returnDateTime, r.inventoryID, category, title, status " +
                                "from rental_transaction r, user, member, media, inventory_item i, status_history s, status " +
                                "where userID = member.memberID and r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and " +
                                "r.rentalID = s.rentalTransactionID and s.statusID = status.statusID;";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
	                        var memberIdOrdinal = reader.GetOrdinal("memberID");
                            var emailOrdinal = reader.GetOrdinal("email");
                            var rentalIdOrdinal = reader.GetOrdinal("rentalID");
                            var rentalDateOrdinal = reader.GetOrdinal("rentalDateTime");
                            var returnDateOrdinal = reader.GetOrdinal("returnDateTime");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");
                            var categoryOrdinal = reader.GetOrdinal("category");
                            var titleOrdinal = reader.GetOrdinal("title");
                            var statusOrdinal = reader.GetOrdinal("status");





                            while (reader.Read())
                            {
	                            var memberId = reader.GetInt32(memberIdOrdinal);

                                var memberEmail = reader[emailOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(emailOrdinal);

                                var rentalId = reader.GetInt32(rentalIdOrdinal);

                                var rentalDate = reader.GetDateTime(rentalDateOrdinal);

                                var returnDate = reader.GetDateTime(returnDateOrdinal);

                                var inventoryId = reader.GetInt32(inventoryIdOrdinal);

                                var category = reader[categoryOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(categoryOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(titleOrdinal);

                                var status = reader[statusOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(statusOrdinal);



                                var item = new RentalItem
                                {
                                    MemberId = memberId,
                                    MemberEmail = memberEmail,
                                    RentalId = rentalId,
                                    RentalDate = rentalDate,
                                    ReturnDate = returnDate,
                                    InventoryId = inventoryId,
                                    Category = category,
                                    Title = title,
                                    Status = status
                                };

                                rentedItems.Add(item);
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

            return rentedItems;
        }

        public static int UpdateStatus(int transactionId, string status, int employeeId)
        {

            var rowsEffected = 0;

            var statusId = 0;

            if (status == "Ordered")
            {
	            statusId = 1; 
            } else if (status == "Shipped")
            {
	            statusId = 2;
            } else if (status == "Delivered")
            {
	            statusId = 3;
            } else if (status == "Returned")
            {
	            statusId = 4;
            }
            else
            {
	            statusId = 5;
            }


            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
	                    var query =
		                    "insert into status_history values (@transactionId, @statusID, @updateDateTime, @employeeID)";

	                    using (var cmd = new MySqlCommand(query, conn))
	                    {
		                    cmd.Transaction = transaction;
		                    cmd.Parameters.AddWithValue("@statusID", statusId);
		                    cmd.Parameters.AddWithValue("@transactionId", transactionId);
		                    cmd.Parameters.AddWithValue("@updateDateTime", DateTime.Now);
		                    cmd.Parameters.AddWithValue("@employeeID", employeeId);

		                    if (cmd.ExecuteNonQuery() != 1)
		                    {
                                transaction.Rollback();
		                    }

		                    if (statusId == 4)
		                    {
			                    cmd.Parameters.Clear();

			                    cmd.CommandText =
                                    "update inventory_item set inStock = true where inventoryID = (select inventoryID from rental_transaction where rentalID = @transactionId order by rentalID DESC limit 1);";
			                    cmd.Parameters.AddWithValue("@transactionId", transactionId);


                                if (cmd.ExecuteNonQuery() != 1)
			                    {
				                    transaction.Rollback();
			                    }

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

            return rowsEffected;
        }

    }
}
