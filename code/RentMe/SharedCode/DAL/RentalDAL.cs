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
                    var query = "select r.memberID, email, r.rentalID, r.rentalDateTime, " +
                                "r.returnDateTime, r.inventoryID, category, title, status from rental_transaction " +
                                "r, user, member, media, inventory_item i, status where " +
                                "userID = member.memberID and member.memberID = r.memberID and " +
                                "r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and " +
                                "status.statusID = (select max(s1.statusID) from status_history s1 " +
                                "where r.rentalID = s1.rentalTransactionID " +
                                "group by s1.rentalTransactionID);";
                    
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

        /// <summary>
        /// Retrieves select borrowed items with the given status.
        /// </summary>
        /// <param name="selectedStatus"> the desired status</param>
        /// <returns>select borrowed items with the given status </returns>
        public static List<RentalItem> RetrieveSelectRentedItems(string selectedStatus)
        {
            var rentedItems = new List<RentalItem>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select r.memberID, email, r.rentalID, r.rentalDateTime, " +
                                "r.returnDateTime, r.inventoryID, category, title, status from rental_transaction " +
                                "r, user, member, media, inventory_item i, status where " +
                                "userID = member.memberID and member.memberID = r.memberID and " +
                                "r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and status.status = @selectedStatus and " +
                                "status.statusID = (select max(s1.statusID) from status_history s1 " +
                                "where r.rentalID = s1.rentalTransactionID " +
                                "group by s1.rentalTransactionID);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {

	                    cmd.Parameters.AddWithValue("@selectedStatus", selectedStatus);

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

        /// <summary>
        /// Updates the status of the given rental
        /// </summary>
        /// <param name="transactionId">the rental transaction id</param>
        /// <param name="status"> the status </param>
        /// <param name="employeeId">the employee updating the rental</param>
        /// <returns>the rows affected</returns>
        public static int UpdateStatus(int transactionId, string status, int employeeId)
        {

            var rowsEffected = 0;

            var statusId = getStatusId(status);

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

		                    if (statusId == 2) { 

			                    updateInventoryItemForShippedStatus(transactionId, statusId, cmd, transaction);
							}

		                    if (statusId == 4)
		                    {
			                    updateInventoryItemForReturnedStatus(transactionId, statusId, cmd, transaction);
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

        private static void updateInventoryItemForReturnedStatus(int transactionId, int statusId, MySqlCommand cmd,
	        MySqlTransaction transaction)
        {
	       
	        cmd.Parameters.Clear();

	        cmd.CommandText =
			        "update inventory_item set inStock = true, isRented = false where inventoryID = (select inventoryID from rental_transaction where rentalID = @transactionId order by rentalID DESC limit 1);";
	        cmd.Parameters.AddWithValue("@transactionId", transactionId);


	        if (cmd.ExecuteNonQuery() != 1) { 
		        transaction.Rollback();
	        }
        }

        private static void updateInventoryItemForShippedStatus(int transactionId, int statusId, MySqlCommand cmd,
	        MySqlTransaction transaction)
        {
	        cmd.Parameters.Clear();

	        cmd.CommandText =
			        "update inventory_item set inStock = false, isRented = true where inventoryID = (select inventoryID from rental_transaction where rentalID = @transactionId order by rentalID DESC limit 1);";
	        cmd.Parameters.AddWithValue("@transactionId", transactionId);


	        if (cmd.ExecuteNonQuery() != 1)
	        {
		        transaction.Rollback();
	        }
        }

        private static int getStatusId(string status)
        {
            int statusId;

            if (status.Equals("Ordered") )
	        {
		        statusId = 1;
	        }
	        else if (status.Equals("Shipped"))
	        {
		        statusId = 2;
	        }
	        else if (status.Equals("Delivered"))
	        {
		        statusId = 3;
	        }
	        else if (status.Equals("Returned"))
	        {
		        statusId = 4;
	        }
	        else
	        {
		        statusId = 5;
	        }

	        return statusId;

        }

    }

}
