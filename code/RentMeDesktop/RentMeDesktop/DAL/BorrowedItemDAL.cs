using MySql.Data.MySqlClient;
using RentMeDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.DAL
{
    class BorrowedItemDAL
    {
        /// <summary>
        /// Retrieves all borrowed items.
        /// </summary>
        /// <returns>All borrowed items </returns>
        public static List<BorrowedItem> RetrieveAllBorrowedItems()
        {
            var borrowedItems = new List<BorrowedItem>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from BorrowedItem";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var trasactionIdOrdinal = reader.GetOrdinal("TransactionID");
                            var itemIdOrdinal = reader.GetOrdinal("ItemId");
                            var customerEmailOrdinal = reader.GetOrdinal("CustomerEmail");
                            var rentalDateOrdinal = reader.GetOrdinal("RentalDate");
                            var returnDateOrdinal = reader.GetOrdinal("ReturnDate");
                            var statusOrdinal = reader.GetOrdinal("Status");



                            while (reader.Read())
                            {
                                var transationId = reader.GetInt32((trasactionIdOrdinal));
                                var itemId = reader.GetInt32((itemIdOrdinal));

                                var customerEmail = reader[customerEmailOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(customerEmailOrdinal);

                                var rentalDate = reader.GetDateTime(rentalDateOrdinal);
                                var returnDate = reader.GetDateTime(returnDateOrdinal);
                                var status = reader.GetString(statusOrdinal);



                                var item = new BorrowedItem
                                {
                                    TrasactionId = transationId,
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
    }
}
