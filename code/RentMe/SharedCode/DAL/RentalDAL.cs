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
	                        var memberIDOrdinal = reader.GetOrdinal("memberID");
                            var emailOrdinal = reader.GetOrdinal("email");
                            var rentalIDOrdinal = reader.GetOrdinal("rentalID");
                            var rentalDateOrdinal = reader.GetOrdinal("rentalDateTime");
                            var returnDateOrdinal = reader.GetOrdinal("returnDateTime");
                            var inventoryIDOrdinal = reader.GetOrdinal("inventoryID");
                            var categoryOrdinal = reader.GetOrdinal("category");
                            var titleOrdinal = reader.GetOrdinal("title");
                            var statusOrdinal = reader.GetOrdinal("status");





                            while (reader.Read())
                            {
	                            var memberID = reader.GetInt32(memberIDOrdinal);

                                var memberEmail = reader[emailOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(emailOrdinal);

                                var rentalID = reader.GetInt32(rentalIDOrdinal);

                                var rentalDate = reader.GetDateTime(rentalDateOrdinal);

                                var returnDate = reader.GetDateTime(returnDateOrdinal);

                                var inventoryID = reader.GetInt32(inventoryIDOrdinal);

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
                                    MemberId = memberID,
                                    MemberEmail = memberEmail,
                                    RentalId = rentalID,
                                    RentalDate = rentalDate,
                                    ReturnDate = returnDate,
                                    InventoryId = inventoryID,
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

        public static int UpdateStatus(int transactionId, string status)
        {

            var rowsEffected = 0;

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    const string query = "UPDATE BorrowedItem SET Status = @status WHERE TransactionID = @transactionId;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@transactionId", transactionId);

                        rowsEffected = cmd.ExecuteNonQuery();

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
