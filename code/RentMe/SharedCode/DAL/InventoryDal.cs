﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SharedCode.Model;

namespace SharedCode.DAL
{
    /// <summary>
    /// The inventory dal class responsible for communicating with the database for inventory items 
    /// </summary>
    public class InventoryDal : IInventoryDal
    {
        /// <summary>
        /// Gets the inventory items from the database that are in stock or rented 
        /// </summary>
        /// <returns>The inventory items from the database or an error if something goes wrong on the database </returns>
        public List<InventoryItem> GetInventoryItems()
        {
            var inventory = new List<InventoryItem>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from inventory_item i, media m where i.mediaID = m.mediaID and m.mediaID not in (select mediaID from media where inStock = 0 and isRented = 0)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var titleOrdinal = reader.GetOrdinal("title");
                            var categoryOrdinal = reader.GetOrdinal("category");
                            var typeOrdinal = reader.GetOrdinal("type");
                            var conditionOrdinal = reader.GetOrdinal("condition");
                            var inStockOrdinal = reader.GetOrdinal("inStock");
                            var isRentedOrdinal = reader.GetOrdinal("isRented");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");
                            var mediaIdOrdinal = reader.GetOrdinal("mediaID");

                            while (reader.Read())
                            {
                                var title = reader[titleOrdinal] == DBNull.Value ? "null" : reader.GetString(titleOrdinal);
                                var category = reader[categoryOrdinal] == DBNull.Value ? "null" : reader.GetString(categoryOrdinal);
                                var type = reader[typeOrdinal] == DBNull.Value ? "null" : reader.GetString(typeOrdinal);
                                var condition = reader[conditionOrdinal] == DBNull.Value ? "null" : reader.GetString(conditionOrdinal);
                                var inStock = reader.GetInt32(inStockOrdinal);
                                var isRented = reader.GetInt32(isRentedOrdinal);
                                var inventoryId = reader.GetInt32(inventoryIdOrdinal);
                                var mediaId = reader.GetInt32(mediaIdOrdinal);



                                var inventoryItem = new InventoryItem
                                {
                                    Title = title,
                                    Category = category,
                                    Type = type,
                                    Condition = condition,
                                    IsRented = isRented == 1,
                                    InStock = inStock == 1,
                                    InventoryId = inventoryId,
                                    MediaId = mediaId
                                };

                                inventory.Add(inventoryItem);
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

            return inventory;
        }

        /// <summary>
        /// Gets history of an item and returns a list of rental items for that item
        /// </summary>
        /// <returns>A list of rental items with information about a specified inventory item</returns>
        public List<RentalItem> GetItemHistorySummary(int inventoryId)
        {
            List<RentalItem> rentalItems = new List<RentalItem>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select r.memberID, s2.`condition`, email, r.rentalID, r.rentalDateTime, r.returnDateTime, " +
                                "r.inventoryID, category, title, status, s2.updateDateTime from rental_transaction r, " +
                                "user, member, media, inventory_item i, status_history s2, status where userID = member.memberID and " +
                                "member.memberID = r.memberID and r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and " +
                                "i.inventoryID = @inventoryId and s2.rentalTransactionID = r.rentalID and s2.statusID = status.statusID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@inventoryId", MySqlDbType.Int32);
                        cmd.Parameters["@inventoryId"].Value = inventoryId;

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
                            var conditionOrdinal = reader.GetOrdinal("condition");
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
                                var condition = reader[conditionOrdinal] == DBNull.Value
	                                ? "null"
	                                : reader.GetString(conditionOrdinal);


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
                                    UpdateDateTime = updateDateTime,
                                    Condition = condition
                                };

                                rentalItems.Add(rentalItem);
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

            return rentalItems.OrderByDescending(item => item.RentalDate).ThenByDescending(item => item.UpdateDateTime).ToList();
        }

        /// <summary>
        /// Adds an inventory item to the DB
        /// </summary>
        /// <param name="item">the item being added</param>
        /// @precondition none
        /// @postcondition the item is added or an error is thrown if something went wrong
        public void AddInventoryItem(InventoryItem item)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {

                        var query = "select mediaID from media where LOWER(type) like concat('%', @type, '%') and " +
                                    "LOWER(title) like concat('%', @title, '%') " +
                                    "and LOWER(category) like concat('%', @category, '%')";

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Transaction = transaction;

                            var mediaId = -1;
                            cmd.Parameters.AddWithValue("@type", item.Type.ToLower());
                            cmd.Parameters.AddWithValue("@title", item.Title.ToLower());
                            cmd.Parameters.AddWithValue("@category", item.Category.ToLower());

                            using (var reader = cmd.ExecuteReader())
                            {
	                            var idOrdinal = reader.GetOrdinal("mediaID");

	                            while (reader.Read())
	                            {
		                            mediaId = reader.GetInt32(idOrdinal);
	                            }
                            }

                            if (mediaId == -1)
                            {
	                            addItemToDbWhenItemDoesNotExistInMediaTableAlready(item, cmd, transaction);
                            }
                            else
                            {
	                            addItemToDbWhenItemDoesExist(item, cmd, mediaId, transaction);
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

        /// <summary>
        /// Removes the inventory item from the db
        /// </summary>
        /// <param name="inventoryId"> the inventory id of the item to be removed</param>
        /// @precondition none
        /// @postcondition the item is removed or an error is thrown if something went wrong
        public void RemoveInventoryItem(int inventoryId)
        {
	        try
	        {
		        var conn = DbConnection.GetConnection();
		        using (conn)
		        {
			        conn.Open();
			        var query = "update inventory_item set inStock = 0, isRented = 0 where inventoryID = @inventoryID;";
			        using (var cmd = new MySqlCommand(query, conn))
			        {
				        cmd.Parameters.AddWithValue("@inventoryID", inventoryId);
				        cmd.ExecuteScalar();
			        }
			        conn.Close();

		        }
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
        }

        private static void addItemToDbWhenItemDoesExist(InventoryItem item, MySqlCommand cmd, int mediaId,
	        MySqlTransaction transaction)
        {
	        cmd.Parameters.Clear();
	        cmd.CommandText =
		        "insert into inventory_item(mediaID, `condition`, inStock, isRented) values (@mediaId, @condition, @inStock, @isRented);";

	        cmd.Parameters.AddWithValue("@mediaId", mediaId);
	        cmd.Parameters.AddWithValue("@condition", item.Condition);
	        cmd.Parameters.AddWithValue("@inStock", 1);
	        cmd.Parameters.AddWithValue("@isRented", 0);


            if (cmd.ExecuteNonQuery() != 1)
	        {
		        transaction.Rollback();
	        }
        }

        private static void addItemToDbWhenItemDoesNotExistInMediaTableAlready(InventoryItem item, MySqlCommand cmd,
	        MySqlTransaction transaction)
        {
	        cmd.Parameters.Clear();
            
		        cmd.CommandText =
			        "insert into media(type, title, category) values (@type, @title, @category)";

		        cmd.Parameters.AddWithValue("@type", item.Type);
		        cmd.Parameters.AddWithValue("@title", item.Title);
		        cmd.Parameters.AddWithValue("@category", item.Category);

		        if (cmd.ExecuteNonQuery() != 1)
		        {
			        transaction.Rollback();
		        }

		        cmd.Parameters.Clear();
		        cmd.CommandText =
                    "insert into inventory_item(mediaID, `condition`, inStock, isRented) values (last_insert_id(), @condition, @inStock, @isRented);";


		        cmd.Parameters.AddWithValue("@condition", item.Condition);
		        cmd.Parameters.AddWithValue("@inStock", 1);
		        cmd.Parameters.AddWithValue("@isRented", 0);


            if (cmd.ExecuteNonQuery() != 1)
		        {
			        transaction.Rollback();
		        }
        }
    }
}
