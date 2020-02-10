using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using SharedCode.Model;

namespace SharedCode.DAL
{
    
    public class InventoryDal : IInventoryDal
    {
        public List<InventoryItem> GetInventoryItems()
        {
            var inventory = new List<InventoryItem>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from inventory_item i, media m where i.mediaID = m.mediaID";
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
    }
}
