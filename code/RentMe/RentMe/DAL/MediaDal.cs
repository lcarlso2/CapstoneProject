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
	public class MediaDal : IMediaDal
	{

        /// <summary>
        /// Retrieves all media from the database 
        /// </summary>
        /// <returns>The list of media objects on the database, or throws an error because something went wrong on the database side</returns>
        public List<Media> RetrieveAllMedia()
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from inventory_item, media where inventory_item.mediaID = media.mediaID and inStock = true and isRented = false";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using var reader = cmd.ExecuteReader();
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
        /// Retrieves all media with the given category
        /// </summary>
        /// <param name="category">the desired category</param>
        /// <returns>the media matching that category or an error is thrown if something goes wrong on the database</returns>
        public List<Media> RetrieveMediaByCategory(string category)
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from media, inventory_item where Category = @category and media.mediaID = inventory_item.mediaID and inStock = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@category", MySqlDbType.VarChar);
                        cmd.Parameters["@category"].Value = category;

                        using var reader = cmd.ExecuteReader();
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
        /// Retrieves all media with the given type
        /// </summary>
        /// <param name="type">the desired type</param>
        /// <returns>the media matching that type or an error is thrown if something goes wrong on the database</returns>
        public List<Media> RetrieveMediaByType(string type)
        {
            var mediaItems = new List<Media>();

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from media, inventory_item where type = @type and media.mediaID = inventory_item.mediaID and inStock = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@type", MySqlDbType.VarChar);
                        cmd.Parameters["@type"].Value = type;

                        using var reader = cmd.ExecuteReader();
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
