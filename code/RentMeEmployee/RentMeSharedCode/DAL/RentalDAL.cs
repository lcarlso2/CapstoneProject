using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RentMeSharedCode.Model;

namespace RentMeSharedCode.DAL
{
    public class RentalDAL
    {

        /// <summary>
        /// Retrieves all borrowed items.
        /// </summary>
        /// <returns>All borrowed items </returns>
        public static List<RentalItem> RetrieveAllBorrowedItems()
        {
            var borrowedItems = new List<RentalItem>();

            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from BorrowedItem";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var transactionIdOrdinal = reader.GetOrdinal("TransactionID");
                            var itemIdOrdinal = reader.GetOrdinal("ItemId");
                            var customerEmailOrdinal = reader.GetOrdinal("CustomerEmail");
                            var rentalDateOrdinal = reader.GetOrdinal("RentalDate");
                            var returnDateOrdinal = reader.GetOrdinal("ReturnDate");
                            var statusOrdinal = reader.GetOrdinal("Status");



                            while (reader.Read())
                            {
                                var transationId = reader.GetInt32((transactionIdOrdinal));
                                var itemId = reader.GetInt32((itemIdOrdinal));

                                var customerEmail = reader[customerEmailOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(customerEmailOrdinal);

                                var rentalDate = reader.GetDateTime(rentalDateOrdinal);
                                var returnDate = reader.GetDateTime(returnDateOrdinal);
                                var status = reader.GetString(statusOrdinal);



                                var item = new RentalItem
                                {
                                    TransactionId = transationId,
                                    ItemId = itemId,
                                    CustomerEmail = customerEmail,
                                    RentalDate = rentalDate,
                                    ReturnDate = returnDate,
                                    Status = status
                                };

                                borrowedItems.Add(item);
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

            return borrowedItems;
        }

        public static int UpdateStatus(int transactionId, string status)
        {

            var rowsEffected = 0;

            try
            {
                var conn = DBConnection.GetConnection();
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
