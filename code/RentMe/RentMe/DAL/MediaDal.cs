using MySql.Data.MySqlClient;
using RentMe.Models;
using SharedCode.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.DAL
{
	/// <summary>
	/// The class responsible communicating with the DB for the media 
	/// </summary>
	public class MediaDal
	{

        /// <summary>
        /// Retrieves all media.
        /// </summary>
        /// <returns>All media </returns>
        public static List<Media> RetrieveAllMedia()
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from inventory_item, media where inventory_item.mediaID = media.mediaID and inStock = true";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var categoryOrdinal = reader.GetOrdinal("category");
                            var typeOrdinal = reader.GetOrdinal("type");
                            var titleOrdinal = reader.GetOrdinal("title");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");



                            while (reader.Read())
                            {
                                var inventoryId = reader.GetInt32((inventoryIdOrdinal));

                                var category = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var type = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);



	                            var media = new Media { InventoryId = inventoryId, Category = category, Title = title, Type = type };

	                            mediaItems.Add(media);
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

            return mediaItems;
        }

        /// <summary>
        /// Retrieves all media in a specific category.
        /// </summary>
        /// <returns>All media in a category</returns>
        public static List<Media> RetrieveMediaByCategory(string category)
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from media, inventory_item where Category = @category and media.mediaID = inventory_item.mediaID and inStock = 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@category", MySqlDbType.VarChar);
                        cmd.Parameters["@category"].Value = category;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var categoryOrdinal = reader.GetOrdinal("Category");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var typeOrdinal = reader.GetOrdinal("Type");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");



                            while (reader.Read())
                            {
                                var inventoryId = reader.GetInt32((inventoryIdOrdinal));

                                var categoryValue = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var type = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);

                          
	                            var media = new Media { InventoryId = inventoryId, Category = categoryValue, Title = title, Type = type };

	                            mediaItems.Add(media);
                                
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

            return mediaItems;
        }

        /// <summary>
        /// Retrieves all media in a specific type.
        /// </summary>
        /// <returns>All media in a type</returns>
        public static List<Media> RetrieveMediaByType(string type)
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from media, inventory_item where type = @type and media.mediaID = inventory_item.mediaID and inStock = 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@type", MySqlDbType.VarChar);
                        cmd.Parameters["@type"].Value = type;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var categoryOrdinal = reader.GetOrdinal("Category");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var typeOrdinal = reader.GetOrdinal("Type");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");
                      



                            while (reader.Read())
                            {
                                var inventoryId = reader.GetInt32((inventoryIdOrdinal));

                                var category = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var typeValue = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);


                                var media = new Media { InventoryId = inventoryId, Category = category, Title = title, Type = typeValue };

	                            mediaItems.Add(media);
                                
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

            return mediaItems;
        }

    }
}
