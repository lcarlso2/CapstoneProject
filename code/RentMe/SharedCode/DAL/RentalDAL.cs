﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SharedCode.Model;

namespace SharedCode.DAL
{
	/// <summary>
	/// The rental dal responsible for communicating with the database for the rentals 
	/// </summary>
	public class RentalDal : IRentalDal
	{


		/// <summary>
		/// Retrieves all borrowed items.
		/// </summary>
		/// <returns>All borrowed items or an error if something goes wrong on the database</returns>
        public List<RentalItem> RetrieveAllRentedItems()
        {
            var rentedItems = new List<RentalItem>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select DISTINCT r.memberID, email, status_history.`condition`, r.rentalID, r.rentalDateTime, " +
                                "r.returnDateTime, r.inventoryID, category, title, status from rental_transaction " +
                                "r, user, member, media, inventory_item i, status, status_history where " +
                                "userID = member.memberID and member.memberID = r.memberID and " +
                                "r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and status_history.statusID = status.statusID and " +
                                "status_history.rentalTransactionID = r.rentalID and " +
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
                            var conditionOrdinal = reader.GetOrdinal("condition");





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

                                var condition = reader[conditionOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(conditionOrdinal);



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
                                    Status = status,
                                    Condition = condition
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

            return rentedItems.OrderByDescending(item => item.RentalDate).ToList();
        }

        /// <summary>
        /// Retrieves all rentals with the given customer email
        /// </summary>
        /// <param name="email">the customer's email</param>
        /// <returns>the list of rental items from the database rented by the given customer. Or an error if something goes wrong on the database</returns>
        public List<RentalItem> RetrieveAllRentalsByCustomer(string email)
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
                                "where r.rentalID = s1.rentalTransactionID and r.memberId = (select memberID from member where email = @email) " +
                                "group by s1.rentalTransactionID);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

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

            return rentedItems.OrderByDescending(item => item.RentalDate).ToList();
        }


        /// <summary>
        /// Retrieves select borrowed items with the given status.
        /// </summary>
        /// <param name="selectedStatus"> the desired status</param>
        /// <returns>select borrowed items with the given status or an error if something goes wrong on the database</returns>
        public List<RentalItem> RetrieveSelectRentedItems(string selectedStatus)
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

            return rentedItems.OrderByDescending(item => item.RentalDate).ToList();
        }

        /// <summary>
        /// Updates the status of the given rental
        /// </summary>
        /// <param name="transactionId">the rental transaction id</param>
        /// <param name="status"> the status </param>
        /// <param name="employeeId">the employee updating the rental</param>
        /// <param name="condition">the condition of the rental</param>
        /// <returns>the rows affected or an error if something goes wrong on the database</returns>
        public int UpdateStatus(int transactionId, string status, int employeeId, string condition)
        {

	        var rowsEffected = 0;

	        var statusId = RentalItem.GetStatusId(status);

	        try
	        {
		        var conn = DbConnection.GetConnection();
		        using (conn)
		        {
			        conn.Open();
			        using (var transaction = conn.BeginTransaction())
			        {
				        var query =
					        "insert into status_history values (@transactionId, @statusID, @updateDateTime, @employeeID, @condition)";

				        using (var cmd = new MySqlCommand(query, conn))
				        {
					        cmd.Transaction = transaction;
					        cmd.Parameters.AddWithValue("@statusID", statusId);
					        cmd.Parameters.AddWithValue("@transactionId", transactionId);
					        cmd.Parameters.AddWithValue("@updateDateTime", DateTime.Now);
					        cmd.Parameters.AddWithValue("@employeeID", employeeId);
					        cmd.Parameters.AddWithValue("@condition", condition);

                            if (cmd.ExecuteNonQuery() != 1)
					        {
						        transaction.Rollback();
					        }

					        if (statusId == 2)
					        {

						        updateInventoryItemForShippedStatus(transactionId, cmd, transaction);
                                updateCondition(transactionId, cmd, transaction, condition);
					        }

					        if (statusId == 4)
					        {
						        insertIntoReturnConditionTable(transactionId, cmd, transaction, condition);
						        updateInventoryItemForReturnedStatus(transactionId, cmd, transaction,
							        condition);
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


        /// <summary>
        /// Gets the history for a specific rental transaction id
        /// </summary>
        /// <param name="rentalId"> the desired rental id</param>
        /// <returns>the history for the specific rental transaction or an error if something went wrong on the DB</returns>
        public List<RentalItem> RetrieveHistoryForRentalTransaction(int rentalId)
        {
	        var rentalHistory = new List<RentalItem>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select r.memberID, email, r.rentalID, r.rentalDateTime, r.returnDateTime, " +
                                "r.inventoryID, category, title, status, s2.updateDateTime from rental_transaction r, " +
                                "user, member, media, inventory_item i, status_history s2, status where userID = member.memberID " +
                                "and member.memberID = r.memberID and r.inventoryID = i.inventoryID and i.mediaID = media.mediaID " +
                                "and s2.rentalTransactionID = @rentalId and s2.rentalTransactionID = r.rentalID and s2.statusID = status.statusID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

	                    cmd.Parameters.AddWithValue("@rentalId", rentalId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            var memberIdOrdinal = reader.GetOrdinal("memberID");
                            var emailOrdinal = reader.GetOrdinal("email");
                            var rentalIdOrdinal = reader.GetOrdinal("rentalID");
                            var rentalDateTimeOrdinal = reader.GetOrdinal("rentalDateTime");
                            var returnDateTimeOrdinal = reader.GetOrdinal("returnDateTime");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");
                            var categoryOrdinal = reader.GetOrdinal("category");
                            var titleOrdinal = reader.GetOrdinal("title");
                            var statusOrdinal = reader.GetOrdinal("status");
                            var updateDateTimeOrdinal = reader.GetOrdinal("updateDateTime");

                            while (reader.Read())
                            {
                                var memberID = reader.GetInt32(memberIdOrdinal);
                                var email = reader[emailOrdinal] == DBNull.Value ? "null" : reader.GetString(emailOrdinal);
                                var rentalID = reader.GetInt32(rentalIdOrdinal);
                                var rentalDateTime = reader.GetDateTime(rentalDateTimeOrdinal);
                                var returnDateTime = reader.GetDateTime(returnDateTimeOrdinal);
                                var inventoryID = reader.GetInt32(inventoryIdOrdinal);
                                var category = reader[categoryOrdinal] == DBNull.Value ? "null" : reader.GetString(categoryOrdinal);
                                var title = reader[titleOrdinal] == DBNull.Value ? "null" : reader.GetString(titleOrdinal);
                                var status = reader[statusOrdinal] == DBNull.Value ? "null" : reader.GetString(statusOrdinal);
                                var updateDateTime = reader.GetDateTime(updateDateTimeOrdinal);


                                var rentalItem = new RentalItem
                                {
                                    MemberId = memberID,
                                    MemberEmail = email,
                                    RentalId = rentalID,
                                    RentalDate = rentalDateTime,
                                    ReturnDate = returnDateTime,
                                    InventoryId = inventoryID,
                                    Category = category,
                                    Title = title,
                                    Status = status,
                                    UpdateDateTime = updateDateTime
                                };

                                rentalHistory.Add(rentalItem);
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


            return rentalHistory.OrderByDescending(item => item.RentalDate).ThenByDescending(item => item.UpdateDateTime).ToList();

        }

        private static void updateCondition(int transactionId, MySqlCommand cmd,
	        MySqlTransaction transaction, string condition)
        {
	        cmd.Parameters.Clear();

	        cmd.CommandText =
		        "update inventory_item set `condition` = @condition where inventoryID = (select inventoryID from rental_transaction where rentalID = @transactionId order by rentalID DESC limit 1);";
	        cmd.Parameters.AddWithValue("@transactionId", transactionId);
	        cmd.Parameters.AddWithValue("@condition", condition);


	        if (cmd.ExecuteNonQuery() != 1)
	        {
		        transaction.Rollback();
	        }
        }

        private static void updateInventoryItemForReturnedStatus(int transactionId, MySqlCommand cmd,
	        MySqlTransaction transaction, string condition)
        {
	       
	        cmd.Parameters.Clear();

	        cmd.CommandText =
			        "update inventory_item set inStock = true, isRented = false, `condition` = @condition where inventoryID = (select inventoryID from rental_transaction where rentalID = @transactionId order by rentalID DESC limit 1);";
	        cmd.Parameters.AddWithValue("@transactionId", transactionId);
	        cmd.Parameters.AddWithValue("@condition", condition);


            if (cmd.ExecuteNonQuery() != 1) { 
		        transaction.Rollback();
	        }
        }

        private static void insertIntoReturnConditionTable(int transactionId, MySqlCommand cmd, MySqlTransaction transaction,
	        string condition)
        {
	        cmd.Parameters.Clear();

	        cmd.CommandText = "insert into return_condition values (@rentalID, @condition, " +
	                          "(select `condition` from inventory_item, rental_transaction where " +
	                          "rental_transaction.rentalID = @rentalID and rental_transaction.inventoryID = inventory_item.inventoryID))";
	        cmd.Parameters.AddWithValue("@rentalID", transactionId);
	        cmd.Parameters.AddWithValue("@condition", condition);

	        if (cmd.ExecuteNonQuery() != 1)
	        {
		        transaction.Rollback();
	        }
        }

        private static void updateInventoryItemForShippedStatus(int transactionId, MySqlCommand cmd,
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

        

    }

}
